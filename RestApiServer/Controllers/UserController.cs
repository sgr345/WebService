using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using RestApiServer.Controllers.Interfaces;
using RestApiServer.Models;
using RestApiServer.Models.DataModel;
using RestApiServer.Models.ViewModel;
using RestApiServer.Utilities.Jwt;

namespace RestApiServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly JwtSettings jwtSettings;
        private IUser _user;
        private ILogger<UserController> _logger;
        private HttpContext context;

        public UserController(JwtSettings jwtSettings, IUser user, ILogger<UserController> logger, IHttpContextAccessor accessor)
        {
            this.jwtSettings = jwtSettings;
            _user = user;
            context = accessor.HttpContext;
        }

        #region 内部関数 
        private void SetCookies(string key, string value, int expireTime = 0)
        {
            try
            {
                var option = new CookieOptions();
                option.HttpOnly = true;
                option.SameSite = SameSiteMode.Strict;
                option.Secure = true;
                if (expireTime != 0)
                {
                    option.Expires = DateTime.Now.AddHours(expireTime);
                }
                else
                {
                    option.Expires = DateTime.Now.AddMonths(1);
                }
                Response.Cookies.Append(key, value, option);
                
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        [HttpPost]
        public IActionResult GetToken(LoginInfo loginInfo)
        {
            try
            {
                var Token = new UserTokens();
                if (_user.MatchUserInfo(loginInfo))
                {
                    var user = _user.GetUserInfoByID(loginInfo.UserID);
                    var roles = _user.GetRolesOwnedByUser(loginInfo.UserID);
                    Token = JwtHelpers.GetTokenkey(new UserTokens()
                    {
                        EmailId = user.UserEmail,
                        GuidId = Guid.NewGuid(),
                        UserName = user.UserName,
                        UserId = user.UserID,
                        Role = roles.RoleID,
                    }, jwtSettings);
                    SetCookies("X-Access-Token", Token.Token, 1);
                    Response.Cookies.Append("X-UserID", user.UserID, new CookieOptions() { HttpOnly= false, SameSite= SameSiteMode.Strict });
                    SetCookies("X-Refresh-Token", Token.RefreshToken);
                }
                else
                {
                    return BadRequest("wrong password");
                }
                return Ok(Token);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        [HttpGet]
        public IActionResult Refresh()
        {
            if (!(Request.Cookies.TryGetValue("X-UserID", out var userID) && Request.Cookies.TryGetValue("X-Refresh-Token", out var refreshToken)))
            {
                return BadRequest();
            }

            var user = _user.GetUserInfoByID(userID);

            if (user == null)
            {
                return BadRequest();
            }

            var roles = _user.GetRolesOwnedByUser(userID);
            var Token = JwtHelpers.GetTokenkey(new UserTokens()
            {
                EmailId = user.UserEmail,
                GuidId = Guid.NewGuid(),
                UserName = user.UserName,
                UserId = user.UserID,
                Role = roles.RoleID,
            }, jwtSettings);

            SetCookies("X-Access-Token", Token.Token, 1);
            SetCookies("X-UserID", user.UserID, 1);
            SetCookies("X-Refresh-Token", Token.RefreshToken);
            return Ok();
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            Response.Cookies.Delete("X-Access-Token");
            Response.Cookies.Delete("X-UserID");
            Response.Cookies.Delete("X-Refresh-Token");

            return Ok("LogOut");
        }

        [HttpPost]
        public IActionResult Register(RegisterInfo register)
        {
            try
            {
                if (_user.RegisterUser(register) > 0)
                {
                    return Ok($"Registed to user: {register.UserID}");
                }
                else
                {
                    return BadRequest("Failed to update");
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Withdrawn(UserInfo userInfo)
        {
            try
            {
                if (!Request.Cookies.TryGetValue("X-UserID", out var userID))
                {
                    return BadRequest();
                }
                else
                {
                    userInfo.UserID = userID;
                }

                if (_user.WithdrawnUser(userInfo) > 0)
                {
                    Response.Cookies.Delete("X-Access-Token");
                    Response.Cookies.Delete("X-UserID");
                    Response.Cookies.Delete("X-Refresh-Token");
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetDataAllOfLoginUser()
        {
            return Ok();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "AssociateUser")]
        public IActionResult GetDataForUser()
        {
            return Ok();
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "SystemUser")]
        public IActionResult GetDataForAdmin()
        {
            return Ok();
        }
    }
}

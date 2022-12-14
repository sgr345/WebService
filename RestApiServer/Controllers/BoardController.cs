using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestApiServer.Controllers.Interfaces;
using RestApiServer.Models.ViewModel;

namespace RestApiServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        private ILogger<BoardController> logger;
        private IBoard board;
        private HttpContext _context;

        #region LocalMethod
        private List<string> CheckToken(string userID, string userName)
        {
            List<string> errorList = new List<string>();
            var token = Request.Cookies["X-Access-Token"];
            var handler = new JwtSecurityTokenHandler();
            var claims = handler.ReadJwtToken(token).Claims;
            if (userID != claims.First(claims => claims.Type == "Id").Value.ToString())
            {
                errorList.Add("UserID Error");
            }
            if (userName != claims.First(claims => claims.Type == ClaimTypes.Name).Value.ToString())
            {
                errorList.Add("UserName Error");
            }
            return errorList;
        }
        #endregion

        public BoardController(IHttpContextAccessor accessor, IBoard board, ILogger<BoardController> logger)
        {
            this.logger = logger;
            this.board = board;
            _context = accessor.HttpContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetBoardInfo(string searchSubject, string keyWord, int pageNo = 1, int? pageSize = null)
        {
            var comboList = new Dictionary<string, string>{
                { "Title","Title" },
                { "Content" ,"Content"},
                { "UserName", "UserName"},
                { "Title + Content","Title,Content" }
            };
            
            int itemsPerPage = (int)(pageSize != null && pageSize > 0 ?
                               Convert.ToInt32(pageSize) : 5);
            int numberLinksPerPage = Convert.ToInt32(5);
            var boardInfo = await board.GetBoardList(pageNo, itemsPerPage, numberLinksPerPage, "", "");

            return Ok(boardInfo);
        }
        [HttpGet]
        public async Task<IActionResult> GetBoardDetails(int no)
        {
            string message = string.Empty;

            board.UpdateReadCount(no);
            var result = await board.GetBoardInfo(no);
            
            return Ok(result);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "AssociateUser")]
        public async Task<IActionResult> Post(BoardForm boardForm)
        {
            //var token = Request.Cookies["X-Access-Token"];
            //var handler = new JwtSecurityTokenHandler();
            //var claims = handler.ReadJwtToken(token).Claims;
            //if (boardForm.UserID != claims.First(claims => claims.Type == "Id").Value.ToString())
            //{
            //   return Ok("UserID Error");
            //}
            //if(boardForm.UserName != claims.First(claims => claims.Type == ClaimTypes.Name).Value.ToString())
            //{
            //    return Ok("UserName Error");
            //}

            var validateList = CheckToken(boardForm.UserID, boardForm.UserName);
            if (validateList.Count != 0)
            {
                return Ok(string.Join(',',validateList));
            }

            if (await board.BoardSubmit(boardForm) > 0)
            {
                return Ok("Success");
            }
            else
            {
                return Ok("failed");
            }

        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "AssociateUser")]
        public async Task<IActionResult> Modify(BoardDetails boardDetails)
        {

            var validateList = CheckToken(boardDetails.UserID, boardDetails.UserName);
            if (validateList.Count != 0)
            {
                return Ok(string.Join(',', validateList));
            }

            if (await board.UpdateBoardDetails(boardDetails) > 0)
            {
                return Ok("Success");
            }
            else
            {
                return Ok("Fail");
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme, Roles = "AssociateUser")]
        public async Task<IActionResult> Delete(BoardDetails boardDetails)
        {
            var validateList = CheckToken(boardDetails.UserID, boardDetails.UserName);
            if (validateList.Count != 0)
            {
                return Ok(string.Join(',', validateList));
            }
            if (await board.DeleteBoardDetails(boardDetails) == 0)
            {
                return Ok("Delete Failed");
            }
            return Ok();
        }
    }
}

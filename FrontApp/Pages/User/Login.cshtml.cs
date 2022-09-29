using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IdentityModel.Tokens.Jwt;
using FrontApp.Common;

namespace FrontApp.Pages.User
{
    public class LoginModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (!Request.Cookies.ContainsKey("X-UserName"))
            {
                return Page();
            }
            else
            {
                var handler = new JwtSecurityTokenHandler();
                var result = handler.ReadJwtToken(Request.Cookies["X-Access-Token"]);
                //Common.Common.UserID = result.Claims.First(c => c.Type == "Id").Value;
                //Common.Common.UserName= result.Claims.First(c => c.Type == System.Security.Claims.ClaimTypes.Name).Value;
                //Common.Common.UserRole = result.Claims.First(c => c.Type == System.Security.Claims.ClaimTypes.Role).Value;
                
                return Redirect("/");
            }
        }
    }
}

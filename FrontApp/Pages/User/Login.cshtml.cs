using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontApp.Pages.User
{
    public class LoginModel : PageModel
    {
        private string? UserName { get; set; }

        public IActionResult OnGet()
        {
            if (Request.Cookies.ContainsKey("X-UserName")&&Request.Cookies.ContainsKey("X-UserID"))
            {
                return Redirect("/");
            }
            return Page();
        }

    }
}

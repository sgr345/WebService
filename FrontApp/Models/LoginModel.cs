using System;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontApp.Models
{
    public class LoginModel : PageModel
    {
        public void OnGet()
        {
            var test = Request.PathBase;
        }
    }
}


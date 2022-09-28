using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RestApiServer.Models.ViewModel
{
    public class LoginInfo
    {
        public string UserID { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}


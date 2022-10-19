using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RestApiServer.Models.ViewModel
{
    public class UserInfo
    {
        [Required(ErrorMessage = "Please input your userid")]
        [MinLength(6, ErrorMessage = "Minimum 6")]
        public string UserID { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please input your password")]
        [MinLength(6, ErrorMessage = "Minimum 6")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please input your Name")]
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please input your Email")]
        public string UserEmail { get; set; }

        public virtual ChangeInfo ChangeInfo { get; set; }
    }
}


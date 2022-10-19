using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RestApiServer.Models.ViewModel
{
    public class BoardForm
    {
        [Required(ErrorMessage = "Please Input Title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please Input Content")]
        public string Content { get; set; }

        [Required(ErrorMessage = "UserID Error")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "Name Error")]
        public string UserName { get; set; }
    }
}


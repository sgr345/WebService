
using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace RestApiServer.Models.ViewModel
{
    public class BoardDetails
    {
        public int No { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string UserID { get; set; }

        public string UserName { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}


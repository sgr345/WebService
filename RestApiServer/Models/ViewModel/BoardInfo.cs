using System;
using System.ComponentModel.DataAnnotations;
using RestApiServer.Controllers.Interfaces;
using RestApiServer.Models.DataModel;

namespace RestApiServer.Models.ViewModel
{
    public class BoardInfo
    {
        public List<Board> boardList { get; set; }

        public int TotalEntries { get; set; }

        public int TotalItems { get; set; }

        public int NumberLinksPerPage { get; set; }

        public int CurrentPage { get; set; }

        public int ItemsPerPage { get; set; }

        public int FirstItem { get; set; }

        public int LastItem { get; set; }

        public string SearchSubject { get; set; }

        public string SearchKeyword { get; set; }
    }
}


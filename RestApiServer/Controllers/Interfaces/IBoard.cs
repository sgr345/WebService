using System;
using RestApiServer.Models.ViewModel;

namespace RestApiServer.Controllers.Interfaces
{
    public interface IBoard
    {
        public Task<BoardInfo> GetBoardList(int pageNo, int itemPerPage, int numberLInksPerPage, string cmb, string keyWord);
        public Task<BoardDetails> GetBoardInfo(int no);
        public int UpdateReadCount(int no);
        public Task<int> BoardSubmit(BoardForm form); 
    }
}


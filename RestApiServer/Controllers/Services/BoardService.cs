using System;
using System.Text;
using RestApiServer.Common.Connection;
using RestApiServer.Controllers.Interfaces;
using RestApiServer.Models;
using RestApiServer.Models.DataModel;
using RestApiServer.Models.ViewModel;

namespace RestApiServer.Controllers.Services
{
    public class BoardService : IBoard
    {
        private ILogger<BoardService> _logger;
        private IDBConnection conn;
        public BoardService(IDBConnection conn, ILogger<BoardService> logger)
        {
            this.conn = conn;
            _logger = logger;
        }

        #region private
        private async Task<BoardInfo> GetBoardList(int pageNo, int itemPerPage, int numberLInksPerPage, string searchSubject, string keyWord)
        {
            try
            {
                string whereSql = GetWhereSql(searchSubject, keyWord);

                StringBuilder sql = new StringBuilder();
                sql.AppendLine("SELECT");
                sql.AppendLine("*");
                sql.AppendLine("FROM");
                sql.AppendLine("TBL_BOARD AS BOARD");
                //Covering Index
                sql.AppendLine("INNER JOIN");
                sql.AppendLine("(");
                sql.AppendLine("SELECT");
                sql.AppendLine("No");
                sql.AppendLine("FROM");
                sql.AppendLine("TBL_BOARD");
                sql.AppendLine("WHERE 0=0");

                sql.AppendLine(whereSql);

                sql.AppendLine("ORDER BY No DESC");
                sql.AppendLine($"LIMIT {itemPerPage} OFFSET {(pageNo - 1) * itemPerPage}");
                sql.AppendLine(") AS INDEX_TBL");
                sql.AppendLine("ON");
                sql.AppendLine("INDEX_TBL.No = BOARD.No");

                var boardList = await conn.QueryAsync<Board>(sql.ToString());

                StringBuilder countSql = new StringBuilder();
                countSql.AppendLine("SELECT COUNT(No) FROM TBL_BOARD WHERE 0=0");
                countSql.AppendLine(whereSql);

                int totalBoardCount = await conn.QueryFirstAsync<int>(countSql.ToString());

                var boardInfo = new BoardInfo()
                {
                    boardList = boardList.ToList()
                };

                return boardInfo;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        
        private int DeleteBoardInfo(int no)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("DELETE FROM TBL_BOARD");
                sql.AppendLine("WHERE");
                sql.AppendLine("No = @No");
                return conn.Execute(sql.ToString(), new { No = no });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        
        private int UpdateReadCount(int no)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.AppendLine("UPDATE TBL_BOARD");
                sql.AppendLine("SET");
                sql.AppendLine("ReadCount = ReadCount + 1");
                sql.AppendLine("WHERE");
                sql.AppendLine("No = @No");
                return conn.Execute(sql.ToString(), new { No = no });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }
        private string GetWhereSql(string searchSubject, string keyWord)
        {
            try
            {
                StringBuilder whereSql = new StringBuilder();
                if (!string.IsNullOrEmpty(keyWord))
                {
                    var key = searchSubject.Split(",");
                    for (int i = 0; i < key.Length; i++)
                    {
                        if (i == 0)
                        {
                            whereSql.Append("AND");
                        }
                        else
                        {
                            whereSql.Append("OR");
                        }
                        whereSql.AppendLine($" {key[i]} like '{keyWord}%'");
                    }
                }
                return whereSql.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                throw;
            }
        }

        #endregion

        Task<BoardInfo> IBoard.GetBoardList(int pageNo, int itemPerPage, int numberLInksPerPage, string cmb, string keyWord)
        {
            return GetBoardList(pageNo, itemPerPage, numberLInksPerPage, cmb, keyWord);
        }

        int IBoard.UpdateReadCount(int no)
        {
            return UpdateReadCount(no);
        }
    }
}


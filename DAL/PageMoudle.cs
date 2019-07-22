using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using cms_webservice.IDAL;

namespace cms_webservice.DAL
{
    [Serializable]
    public class PageMoudle : IPageMoudle
    {

        string sql = string.Empty;
        SqlParameter[] sp = null;

        //0:SQL
        const string sqlTotalCount = "SELECT COUNT(*) FROM ({0}) ____TempCountTable";
        //0:SQL
        //1:Sort Fields
        //2:Start Row
        //3:Page Size
        const string sqlPaging = "SELECT * FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY {1}) AS RowRank FROM ({0}) AS _TempTable) AS __TempTable WHERE RowRank > {2} AND RowRank <= {2}+{3}";

        private static DataAccessObjectBase _dao = null;
        public static DataAccessObjectBase DAO
        {
            get
            {
                if (_dao == null)
                    _dao = new DataAccessObjectBase();
                return _dao;
            }
            set
            {
                _dao = value;
            }
        }

        public PageMoudle(string sql)
        {
            this.sql = sql;
        }

        public PageMoudle(string sql, SqlParameter[] sp)
        {
            this.sql = sql;
            this.sp = sp;
        }


        private int pageCount = -1;
        public int PageCount
        {
            get
            {
                if (pageCount == -1)
                {

                    this.TotalCount = int.Parse(DAO.GetSingle(string.Format(sqlTotalCount, this.sql), this.sp).ToString());
                    pageCount = (int)(Math.Ceiling((decimal)(this.TotalCount) / (decimal)(pageSize == 0 ? 1 : pageSize)));
                }

                return pageCount;
            }
        }

        public int TotalCount { get; set; }

        List<SortField> sortField = new List<SortField>();
        public List<SortField> SortField
        {
            get { return sortField; }
            set { sortField = value; }
        }

        private string GetSortFieldsString()
        {
            List<string> temp = new List<string>();
            this.SortField.ForEach(v => {
                if(!v.DESC)
                    temp.Add(v.FieldName);
                else
                    temp.Add(string.Format("{0} DESC", v.FieldName));
            });
            return String.Join(",", temp.ToArray());
        }

        private int pageSize;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }



        int currentPage;
        public int CurrentPage
        {
            get
            {
                return currentPage;
            }
            set
            {
                currentPage = value;
                if (currentPage <= 0)
                {
                    currentPage = 1;
                }

                //0:SQL
                //1:Sort Fields
                //2:Start Row
                //3:Page Size
                currentData = DAO.Select(string.Format(sqlPaging, this.sql, this.GetSortFieldsString(), (this.CurrentPage - 1) * this.PageSize, this.PageSize), this.sp);
            }
        }

        
        DataTable currentData = new DataTable();
        public DataTable CurrentData
        {
            get { return currentData; }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using cms_webservice.IDAL;
using cms_webservice.Model;

namespace cms_webservice.DAL
{
    public class PetitionDAL : IPetitionDAL
    {
        #region SQL
        public const string sqlGetPetitionList = "SELECT * FROM T_Petitions";
        public const string sqlInsertPetition = "INSERT INTO T_Petitions (id, title, description, createTime, fromUserId, toUserId, toUserName, status) VALUES ({0},{1},{2},{3},{4},{5},{6},{7})";
        #endregion


        #region Variables
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
        #endregion


        #region Members


        public DataTable getPetitionList()
        {
            return DAO.Select(sqlGetPetitionList);
        }

        public bool InsertPetition(List<Petition> petitions)
        {
            Dictionary<SqlParameter[], string> insertSqls = new Dictionary<SqlParameter[], string>();

            foreach (Petition a in petitions)
            {
                string sql = string.Format(sqlInsertPetition, "@id", "@title", "@description", "@createTime", "@fromUserId", "@toUserId", "@toUserName", "@status");
                SqlParameter[] parms = new SqlParameter[] {
                    new SqlParameter ("@Title", a.Title),//提问内容
                    new SqlParameter ("@AddTime", a.AddTime),
                    new SqlParameter ("@UpdateTime", a.UpdateTime),
                    new SqlParameter ("@Content", a.Content),//回答
                    new SqlParameter ("@ShortContent", a.ShortContent),//联系方式
                    new SqlParameter ("@ExpireTime", a.ExpireTime),
                    new SqlParameter ("@LongTitle", a.LongTitle)//联系人
                };
                insertSqls.Add(parms, sql);
            }

            return DAO.ExecuteSqlTran(insertSqls);
        }

        #endregion
    }
}

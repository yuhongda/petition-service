using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using cms_webservice.Model;

namespace cms_webservice.DAL
{
    public class HandsUpRecordDAL
    {
        #region SQL
        public const string sqlInsertHandsUpRecord = "INSERT INTO T_HandsUpRecord(userId, petitionId) VALUES({0},{1})";
        public const string sqlDeleteHandsUpRecord = "DELETE FROM T_HandsUpRecord WHERE (userId = {0}) AND (petitionId = {1})";


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

        public bool InsertHandsUpRecord(HandsUpRecord handsUpRecord)
        {
            Dictionary<SqlParameter[], string> insertSqls = new Dictionary<SqlParameter[], string>();

            string sql = string.Format(sqlInsertHandsUpRecord, "@userId", "@petitionId");
            SqlParameter[] parms = {
                new SqlParameter ("@userId", handsUpRecord.UserId),
                new SqlParameter ("@petitionId", handsUpRecord.PetitionId)
            };
            insertSqls.Add(parms, sql);

            return DAO.ExecuteSqlTran(insertSqls);
        }


        public bool DeleteHandsUpRecord(HandsUpRecord handsUpRecord)
        {
            Dictionary<SqlParameter[], string> deleteSqls = new Dictionary<SqlParameter[], string>();

            string sql = string.Format(sqlDeleteHandsUpRecord, "@userId", "@petitionId");
            SqlParameter[] parms = {
                new SqlParameter ("@userId", handsUpRecord.UserId),
                new SqlParameter ("@petitionId", handsUpRecord.PetitionId)
            };
            deleteSqls.Add(parms, sql);

            return DAO.ExecuteSqlTran(deleteSqls);
        }

        #endregion
    }
}

﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using cms_webservice.IDAL;
using cms_webservice.Model;

namespace cms_webservice.DAL
{
    public class HandsUpRecordDAL : IHandsUpRecordDAL
    {
        #region SQL
        public const string sqlSelectHandsUpRecord = "SELECT * FROM T_HandsUpRecord WHERE (userId = {0}) AND (petitionId = {1})";
        public const string sqlGetHandsUpRecordByUserId = "SELECT * FROM T_HandsUpRecord WHERE(userId = {0})";
        public const string sqlGetHandsUpRecordByPetitionId = "SELECT * FROM T_HandsUpRecord WHERE(petitionId = {0})";
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


        public bool CheckIsHandsUp(HandsUpRecord handsUpRecord)
        {
            string sql = string.Format(sqlSelectHandsUpRecord, "@userId", "@petitionId");
            SqlParameter[] parms = {
                new SqlParameter ("@userId", handsUpRecord.UserId),
                new SqlParameter ("@petitionId", handsUpRecord.PetitionId)
            };

            return DAO.CheckExists(sql, parms) != null;
        }

        public DataTable GetHandsUpRecordByUserId(string userId)
        {
            SqlParameter[] sp = {
                new SqlParameter("@userId", userId)
            };
            return DAO.Select(string.Format(sqlGetHandsUpRecordByUserId, "@userId"), sp);
        }

        public DataTable GetHandsUpRecordByPetitionId(int petitionId)
        {
            SqlParameter[] sp = {
                new SqlParameter("@petitionId", petitionId)
            };
            return DAO.Select(string.Format(sqlGetHandsUpRecordByPetitionId, "@petitionId"), sp);
        }
        


        #endregion
    }
}
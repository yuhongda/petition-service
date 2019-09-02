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
        public const string sqlGetPetitionList = "SELECT T_Petitions.*, T_Pic.imageUrl, " +
            "(SELECT COUNT(*) AS Expr1 FROM T_HandsUpRecord WHERE(T_Petitions.id = petitionId)) AS handsUpCount " +
            "FROM T_Petitions LEFT OUTER JOIN T_Pic ON T_Petitions.id = T_Pic.petitionId";
        public const string sqlInsertPetition = "INSERT INTO T_Petitions (title, description, createTime, fromUserId, toUserId, toUserName, status, handsUp) VALUES ({0},{1},{2},{3},{4},{5},{6},0); SELECT CAST(scope_identity() AS int);";
        public const string sqlInsertPic = "INSERT INTO T_Pic (petitionId, imageUrl) VALUES ({0},{1})";
        public const string sqlGetPetitionById = "SELECT T_Petitions.*, T_Pic.imageUrl," +
            "(SELECT COUNT(*) AS Expr1 FROM T_HandsUpRecord WHERE(T_Petitions.id = petitionId)) AS handsUpCount" +
            " FROM T_Petitions LEFT OUTER JOIN T_Pic ON T_Petitions.id = T_Pic.petitionId WHERE (T_Petitions.id = {0})";
        public const string sqlUpdatePetition = "UPDATE T_Petitions SET title = {1}, description = {2}, createTime = {3}, fromUserId = {4}, toUserId = {5}, toUserName = {6}, status = {7}, handsUp = {8} WHERE (id = {0})";
        public const string sqlDeletePic = "DELETE FROM T_Pic WHERE (petitionId = {0})";
        public const string sqlGetPicsByPetitionId = "SELECT * FROM T_Pic WHERE (petitionId = {0})";
        

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

        public DataTable getPicsByPetitionId()
        {
            return DAO.Select(sqlGetPicsByPetitionId);
        }

        public DataTable getPetitionById(int petitionId)
        {
            SqlParameter[] sp = {
                new SqlParameter("@petitionId", petitionId)
            };
            return DAO.Select(string.Format(sqlGetPetitionById, "@petitionId"), sp);
        }

        public bool InsertPetition(Petition petition, List<Pic> pics)
        {
            Dictionary<SqlParameter[], string> insertSqls = new Dictionary<SqlParameter[], string>();
            Int32 newId = 0;

            string sql = string.Format(sqlInsertPetition, "@title", "@description", "@createTime", "@fromUserId", "@toUserId", "@toUserName", "@status");
            SqlParameter[] parms = {
                new SqlParameter ("@title", petition.Title),
                new SqlParameter ("@description", petition.Description),
                new SqlParameter ("@createTime", petition.CreateTime),
                new SqlParameter ("@fromUserId", petition.FromUserId),
                new SqlParameter ("@toUserId", petition.ToUserId),
                new SqlParameter ("@toUserName", petition.ToUserName),
                new SqlParameter ("@status", petition.Status)
            };
            //insertSqls.Add(parms, sql);
            newId = (Int32)DAO.SelectScalar(sql, parms);

            if (pics != null && newId != 0)
            {
                foreach (Pic pic in pics)
                {
                    string sqlPic = string.Format(sqlInsertPic, "@petitionId", "@imageUrl");
                    SqlParameter[] parmsPic = {
                        new SqlParameter ("@petitionId", newId),
                        new SqlParameter ("@imageUrl", pic.ImageUrl)
                    };
                    insertSqls.Add(parmsPic, sqlPic);
                }
            }

            return DAO.ExecuteSqlTran(insertSqls);
        }

        public bool UpdatePetition(Petition petition, List<Pic> pics)
        {
            Dictionary<SqlParameter[], string> updateSqls = new Dictionary<SqlParameter[], string>();

            string sql = string.Format(sqlUpdatePetition, "@id", "@title", "@description", "@createTime", "@fromUserId", "@toUserId", "@toUserName", "@status", "@handsUp");
            SqlParameter[] parms = {
                new SqlParameter ("@id", petition.Id),
                new SqlParameter ("@title", petition.Title),
                new SqlParameter ("@description", petition.Description),
                new SqlParameter ("@createTime", petition.CreateTime),
                new SqlParameter ("@fromUserId", petition.FromUserId),
                new SqlParameter ("@toUserId", petition.ToUserId),
                new SqlParameter ("@toUserName", petition.ToUserName),
                new SqlParameter ("@status", petition.Status),
                new SqlParameter ("@handsUp", petition.HandsUp)
            };
            updateSqls.Add(parms, sql);

            if (pics != null)
            {
                string sqlDelPic = string.Format(sqlDeletePic, "@petitionId");
                SqlParameter[] parmsDelPic = {
                    new SqlParameter ("@petitionId", petition.Id),
                };
                updateSqls.Add(parmsDelPic, sqlDelPic);

                foreach (Pic pic in pics)
                {
                    string sqlPic = string.Format(sqlInsertPic, "@petitionId", "@imageUrl");
                    SqlParameter[] parmsPic = {
                        new SqlParameter ("@petitionId", petition.Id),
                        new SqlParameter ("@imageUrl", pic.ImageUrl)
                    };
                    updateSqls.Add(parmsPic, sqlPic);
                }
            }

            return DAO.ExecuteSqlTran(updateSqls);
        }

        #endregion
    }
}

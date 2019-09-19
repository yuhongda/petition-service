using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using cms_webservice.IDAL;
using cms_webservice.Model;

namespace cms_webservice.DAL
{
    public class UserDAL : IUserDAL
    {
        #region SQL
        public const string sqlInsertUser = "INSERT INTO T_User(phone, username, password, avatar, weiboId) VALUES({0},{1},{2},{3},{4})";
        public const string sqlDeleteUser = "DELETE FROM T_User WHERE (phone = {0})";
        public const string sqlUpdateUser = "UPDATE T_User SET username={1}, password={2}, avatar={3}, weiboId={4} WHERE (phone = {0})";
        public const string sqlCheckUserExists = "SELECT * FROM T_User WHERE (phone = {0})";
        public const string sqlCheckUsernameExists = "SELECT * FROM T_User WHERE (username = {0})";
        public const string sqlCheckUserSignIn = "SELECT * FROM T_User WHERE (phone = {0}) AND (password = {1})";
        public const string sqlSelectUserByPhone = "SELECT * FROM T_User WHERE (phone = {0})";
        public const string sqlSelectUserByWeiboId = "SELECT * FROM T_User WHERE (weiboId = {0})";


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

        public bool InsertUser(User user)
        {
            Dictionary<SqlParameter[], string> insertSqls = new Dictionary<SqlParameter[], string>();

            string sql = string.Format(sqlInsertUser, "@phone", "@username", "@password", "@avatar", "@weiboId");
            SqlParameter[] parms = {
                new SqlParameter ("@phone", user.Phone),
                new SqlParameter ("@username", user.Username),
                new SqlParameter ("@password", user.Password),
                new SqlParameter ("@avatar", user.Avatar),
                new SqlParameter ("@weiboId", user.WeiboId)
            };
            insertSqls.Add(parms, sql);

            return DAO.ExecuteSqlTran(insertSqls);
        }

        public bool DeleteUser(User user)
        {
            Dictionary<SqlParameter[], string> deleteSqls = new Dictionary<SqlParameter[], string>();

            string sql = string.Format(sqlDeleteUser, "@phone");
            SqlParameter[] parms = {
                new SqlParameter ("@phone", user.Phone)
            };
            deleteSqls.Add(parms, sql);

            return DAO.ExecuteSqlTran(deleteSqls);
        }

        public bool UpdateUser(User user)
        {
            Dictionary<SqlParameter[], string> updateSqls = new Dictionary<SqlParameter[], string>();

            string sql = string.Format(sqlUpdateUser, "@phone", "@username", "@password", "@avatar", "@weiboId");
            SqlParameter[] parms = {
                new SqlParameter ("@phone", user.Phone),
                new SqlParameter ("@username", user.Username),
                new SqlParameter ("@password", user.Password),
                new SqlParameter ("@avatar", user.Avatar),
                new SqlParameter ("@weiboId", user.WeiboId)
            };
            updateSqls.Add(parms, sql);

            return DAO.ExecuteSqlTran(updateSqls);
        }

        public bool CheckUserExists(User user)
        {
            string sql = string.Format(sqlCheckUserExists, "@phone");
            SqlParameter[] parms = {
                new SqlParameter ("@phone", user.Phone)
            };

            return DAO.CheckExists(sql, parms) != null;
        }

        public bool CheckUsernameExists(User user)
        {
            string sql = string.Format(sqlCheckUsernameExists, "@username");
            SqlParameter[] parms = {
                new SqlParameter ("@username", user.Username)
            };

            return DAO.CheckExists(sql, parms) != null;
        }

        public bool CheckUserSignIn(User user)
        {
            string sql = string.Format(sqlCheckUserSignIn, "@phone", "@password");
            SqlParameter[] parms = {
                new SqlParameter ("@phone", user.Phone),
                new SqlParameter ("@password", user.Password),
            };

            return DAO.CheckExists(sql, parms) != null;
        }
        

        public DataTable getUserByWeiboId(User user)
        {
            SqlParameter[] sp = {
                new SqlParameter ("@weiboId", user.WeiboId),
            };
            return DAO.Select(string.Format(sqlSelectUserByWeiboId, "@weiboId"), sp);
        }

        public DataTable getUserByPhone(User user)
        {
            SqlParameter[] sp = {
                new SqlParameter ("@phone", user.Phone),
            };
            return DAO.Select(string.Format(sqlSelectUserByPhone, "@phone"), sp);
        }
        


        #endregion
    }
}

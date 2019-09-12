using System;
using System.Data;
using cms_webservice.IDAL;
using cms_webservice.Model;

namespace cms_webservice.BLL
{
    public class UserBLL
    {
        private static readonly IUserDAL dal = cms_webservice.DALFactory.DataAccess.CreateUserDAL();

        public bool InsertUser(User user)
        {
            return dal.InsertUser(user);
        }

        public bool DeleteUser(User user)
        {
            return dal.DeleteUser(user);
        }

        public bool UpdateUser(User user)
        {
            return dal.UpdateUser(user);
        }

        public bool CheckUserExists(User user)
        {
            return dal.CheckUserExists(user);
        }

        public bool CheckUsernameExists(User user)
        {
            return dal.CheckUsernameExists(user);
        }

        public bool CheckUserSignIn(User user)
        {
            return dal.CheckUserSignIn(user);
        }

        public DataTable getUserByPhone(User user)
        {
            return dal.getUserByPhone(user);
        }

        

    }
}

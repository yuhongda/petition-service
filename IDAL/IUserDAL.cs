using System;
using System.Data;
using cms_webservice.Model;

namespace cms_webservice.IDAL
{
    public interface IUserDAL
    {
        bool InsertUser(User user);
        bool DeleteUser(User user);
        bool UpdateUser(User user);
        bool CheckUserExists(User user);
        bool CheckUsernameExists(User user);
        bool CheckUserSignIn(User user);
        DataTable getUserByPhone(User user);
    }
}

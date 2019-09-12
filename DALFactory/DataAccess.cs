using System;
using System.Reflection;

namespace cms_webservice.DALFactory
{
    /// <summary>
    /// DataAccess 的摘要说明。
    /// </summary>
    public sealed class DataAccess
    {
        private static readonly string path = System.Configuration.ConfigurationManager.AppSettings["CMSDAL"];

        private DataAccess() { }

        public static cms_webservice.IDAL.IArticleDAL CreateArticleDAL()
        {
            string className = path + ".ArticleDAL";
            return (cms_webservice.IDAL.IArticleDAL)Assembly.Load(path).CreateInstance(className);
        }

        public static cms_webservice.IDAL.IPetitionDAL CreatePetitionDAL()
        {
            string className = path + ".PetitionDAL";
            return (cms_webservice.IDAL.IPetitionDAL)Assembly.Load(path).CreateInstance(className);
        }

        public static cms_webservice.IDAL.IHandsUpRecordDAL CreateHandsUpRecordDAL()
        {
            string className = path + ".HandsUpRecordDAL";
            return (cms_webservice.IDAL.IHandsUpRecordDAL)Assembly.Load(path).CreateInstance(className);
        }


        public static cms_webservice.IDAL.IUserDAL CreateUserDAL()
        {
            string className = path + ".UserDAL";
            return (cms_webservice.IDAL.IUserDAL)Assembly.Load(path).CreateInstance(className);
        }

    }
}

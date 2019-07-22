using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cms_webservice.IDAL;
using System.Data;
using cms_webservice.Model;

namespace cms_webservice.BLL
{
    public class ArticleBLL
    {
        private static readonly IArticleDAL dal = cms_webservice.DALFactory.DataAccess.CreateArticleDAL();

        public bool CheckExistsArticlesByTitle(string title)
        {
            return dal.CheckExistsArticlesByTitle(title);
        }

        public bool InsertArticles(List<Article> article)
        {
            return dal.InsertArticles(article);
        }
    }
}

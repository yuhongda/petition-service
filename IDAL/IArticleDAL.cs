using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cms_webservice.Model;
using System.Data;

namespace cms_webservice.IDAL
{
    public interface IArticleDAL
    {
        bool InsertArticles(List<Article> article);
        bool CheckExistsArticlesByTitle(string title);
    }
}

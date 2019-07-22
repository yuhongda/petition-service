using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using cms_webservice.BLL;
using cms_webservice.Model;

namespace cms_webservice
{
    /// <summary>
    /// Summary description for Article
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ArticleWebService : System.Web.Services.WebService
    {

        Lazy_Yu<ArticleBLL> lazy_ArticleBLL = new Lazy_Yu<ArticleBLL>(() => new ArticleBLL());
        public ArticleBLL Lazy_ArticleBLL
        {
            get { return this.lazy_ArticleBLL.Value; }
        }

        [WebMethod]
        public string Question(string post)
        {
            Article article = post.FromJsonTo<Article>();
            if (!this.Lazy_ArticleBLL.CheckExistsArticlesByTitle(article.Title))
            {
                if (this.Lazy_ArticleBLL.InsertArticles(new List<Article>() { 
                        new Article(){
                            Title = article.Title,
                            AddTime = DateTime.Now,
                            UpdateTime = DateTime.Now,
                            Content = article.Content,
                            ShortContent = article.ShortContent,
                            ExpireTime = DateTime.Now.AddMonths(12),
                            LongTitle = article.LongTitle
                        }
                    }))
                {
                    return new ErrorCode() { Code = 0 }.ToJSON();
                }
                else
                {
                    return new ErrorCode() { Code = 100 }.ToJSON();//发生错误
                }
            }
            else
            {
                return new ErrorCode() { Code = 200 }.ToJSON();//已存在
            }
            
        }

        [WebMethod(EnableSession = true)]
        public string GetVerifyCode()
        {
            if (this.Session["__VerifyCode@CMS__"] != null)
            {
                return this.Session["__VerifyCode@CMS__"].ToString();
            }
            else
            {
                return string.Empty;
            }

        }
    }

    public class ErrorCode
    {
        public int Code { get; set; }
    }
}

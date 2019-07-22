using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cms_webservice.IDAL;
using System.Data;
using System.Data.SqlClient;
using cms_webservice.Model;

namespace cms_webservice.DAL
{
    public class ArticleDAL : IArticleDAL
    {
        #region SQL
        public const string sqlGetArticlesByTitle = "SELECT * FROM MC_Article WHERE (Title = {0})";
        public const string InsertArticle = "INSERT INTO MC_Article (ColId,Title, TitleFontType, TitleType, UId, UName,UserType,AdminUId,AdminUName,Status,HitCount,AddTime,UpdateTime,TemplatePath,PageType,IsCreated,UserCateId,PointCount,ChargeType,ChargeHourCount,ChargeViewCount,IsOpened,IsDeleted,IsRecommend,IsTop,IsFocus,IsSideShow,IsAllowComment,[Content],ShortContent,IsHeader,HeaderFont,StarLevel,IsShowCommentLink,IsIrregular,IrregularId,ExpireTime,LongTitle) VALUES (297,{0},0,1,1,'admin',1,1,'admin',3,0,{1},{2},'/Template/你问我答_三级.html',1,'TRUE',0,0,1,0,0,2,'FALSE','FALSE','FALSE','FALSE','FALSE','FALSE',{3},{4},'FALSE','|常规|12px|','★★★','FALSE','FALSE',0,{5},{6})";
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


        #region IArticleDAL Members

        public bool CheckExistsArticlesByTitle(string title)
        {
            SqlParameter[] sp = new SqlParameter[]{
                            new SqlParameter("@Title", title)
                        };
            return DAO.CheckExists(string.Format(sqlGetArticlesByTitle, "@Title"), sp) != null;
        }

        public bool InsertArticles(List<Article> articles)
        {
            Dictionary<SqlParameter[], string> insertSqls = new Dictionary<SqlParameter[], string>();

            foreach (Article a in articles)
            {
                string sql = string.Format(InsertArticle, "@Title", "@AddTime", "@UpdateTime", "@Content", "@ShortContent", "@ExpireTime", "@LongTitle");
                SqlParameter[] parms = new SqlParameter[] { 
                    new SqlParameter ("@Title", a.Title),//提问内容
                    new SqlParameter ("@AddTime", a.AddTime),
                    new SqlParameter ("@UpdateTime", a.UpdateTime),
                    new SqlParameter ("@Content", a.Content),//回答
                    new SqlParameter ("@ShortContent", a.ShortContent),//联系方式
                    new SqlParameter ("@ExpireTime", a.ExpireTime),
                    new SqlParameter ("@LongTitle", a.LongTitle)//联系人
                };
                insertSqls.Add(parms, sql);
            }

            return DAO.ExecuteSqlTran(insertSqls);
        }

        #endregion

    }
}

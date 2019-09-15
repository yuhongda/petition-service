using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using cms_webservice.BLL;
using cms_webservice.DAL;
using cms_webservice.Model;

namespace cms_webservice
{
    /// <summary>
    /// Summary description for Petition
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AdminWebService : System.Web.Services.WebService
    {
        Lazy_Yu<PetitionBLL> lazy_PetitionBLL = new Lazy_Yu<PetitionBLL>(() => new PetitionBLL());
        public PetitionBLL Lazy_PetitionBLL
        {
            get { return this.lazy_PetitionBLL.Value; }
        }

        Lazy_Yu<HandsUpRecordBLL> lazy_HandsUpRecordBLL = new Lazy_Yu<HandsUpRecordBLL>(() => new HandsUpRecordBLL());
        public HandsUpRecordBLL Lazy_HandsUpRecordBLL
        {
            get { return this.lazy_HandsUpRecordBLL.Value; }
        }

        Lazy_Yu<UserBLL> lazy_UserBLL = new Lazy_Yu<UserBLL>(() => new UserBLL());
        public UserBLL Lazy_UserBLL
        {
            get { return this.lazy_UserBLL.Value; }
        }

        [WebMethod]
        public string getPetitionList(string post)
        {
            SelectPetitionPostData postData = post.FromJsonTo<SelectPetitionPostData>();
            List<ReturnPetitionList> returnPetitionList = new List<ReturnPetitionList>();

            PageMoudle petitionPageModle = this.Lazy_PetitionBLL.getPetitionList(postData.pageSize, new List<SortField>() {
                new SortField() { FieldName = "id", DESC = true },
            }, postData.reviewStatus);
            petitionPageModle.CurrentPage = postData.currentPage;

            List<PetitionList> petitionList = petitionPageModle.CurrentData.ToList<PetitionList>();
            returnPetitionList = petitionList.GroupBy(
                p => p.Id,
                (key, g) => {
                    PetitionList p = petitionList.FirstOrDefault<PetitionList>(item => item.Id == key);
                    return new ReturnPetitionList()
                    {
                        Id = p.Id,
                        Title = p.Title,
                        Description = p.Description,
                        CreateTime = p.CreateTime,
                        FromUserId = p.FromUserId,
                        ToUserId = p.ToUserId,
                        ToUserName = p.ToUserName,
                        Status = p.Status,
                        HandsUp = p.HandsUp,
                        HandsUpCount = p.HandsUpCount,
                        Pics = g.ToList().Where<PetitionList>(item => item.ImageUrl != null).Select<PetitionList, string>(item => item.ImageUrl).ToList<string>(),
                        FromUserName = p.FromUserName,
                        FromUserAvatar = p.FromUserAvatar
                    };
                }
            ).ToList<ReturnPetitionList>();
            return new ResultWithPage() {
                Code = 0,
                Data = returnPetitionList,
                CurrentPage = petitionPageModle.CurrentPage,
                PageCount = petitionPageModle.PageCount,
                Total = petitionPageModle.TotalCount,
            }.ToJSON();
        }

        [WebMethod]
        public string review(string post)
        {
            InsertPetitionPostData postData = post.FromJsonTo<InsertPetitionPostData>();

            if (this.Lazy_PetitionBLL.UpdatePetition(postData.petition, postData.pics))
            {
                return new Result() { Code = 0, Data = null }.ToJSON();
            }
            else
            {
                return new Result() { Code = 100, Data = null, Message = "发生错误" }.ToJSON();
            }
        }

        [WebMethod]
        public string signIn(string post)
        {
            User postData = post.FromJsonTo<User>();

            if (this.Lazy_UserBLL.CheckUserSignIn(postData))
            {
                List<User> userList = this.Lazy_UserBLL.getUserByPhone(postData).ToList<User>();
                return new ResultUserList() { Code = 0, Data = userList }.ToJSON();
            }
            else
            {
                return new Result() { Code = 101, Data = null, Message = "用户不存在或密码错误" }.ToJSON();
            }
        }
        
    }

}

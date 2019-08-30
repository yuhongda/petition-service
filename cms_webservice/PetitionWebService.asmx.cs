using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using cms_webservice.BLL;
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
    public class PetitionWebService : System.Web.Services.WebService
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

        [WebMethod]
        public string getPetitionList()
        {
            List<ReturnPetitionList> returnPetitionList = new List<ReturnPetitionList>();
            List<PetitionList> petitionList = this.Lazy_PetitionBLL.getPetitionList().ToList<PetitionList>();
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
                        Pics = g.ToList().Where<PetitionList>(item => item.ImageUrl != null).Select<PetitionList, string>(item => item.ImageUrl).ToList<string>()
                    };
                }
            ).ToList<ReturnPetitionList>();
            return new Result() {
                Code = 0,
                Data = returnPetitionList
            }.ToJSON();
        }

        [WebMethod]
        public string getPetitionById(string post)
        {
            SelectPetitionPostData postData = post.FromJsonTo<SelectPetitionPostData>();


            List<ReturnPetitionList> returnPetitionList = new List<ReturnPetitionList>();
            List<PetitionList> petitionList = this.Lazy_PetitionBLL.getPetitionById(postData.petitionId).ToList<PetitionList>();
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
                        Pics = g.ToList().Where<PetitionList>(item => item.ImageUrl != null).Select<PetitionList, string>(item => item.ImageUrl).ToList<string>()
                    };
                }
            ).ToList<ReturnPetitionList>();
            return new Result()
            {
                Code = 0,
                Data = returnPetitionList
            }.ToJSON();
        }

        [WebMethod]
        public string postPetition(string post)
        {
            InsertPetitionPostData postData = post.FromJsonTo<InsertPetitionPostData>();

            if (this.Lazy_PetitionBLL.InsertPetition(postData.petition, postData.pics))
            {
                return new Result() { Code = 0, Data = null }.ToJSON();
            }
            else
            {
                return new Result() { Code = 100, Data = null, Message = "发生错误" }.ToJSON();
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public void upload()
        {
            HttpPostedFile uploadFile = HttpContext.Current.Request.Files["uploadFile"];
            //HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            //HttpContext.Current.Response.AddHeader("Content-Type", "application/json; charset=utf-8");
            HttpContext.Current.Response.ContentType = "application/json";

            if (uploadFile != null)
            {
                if (!System.IO.Directory.Exists(Server.MapPath("~/UploadedFiles")))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath("~/UploadedFiles"));
                }

                string filename = Path.GetFileNameWithoutExtension(uploadFile.FileName);
                string ext = Path.GetExtension(uploadFile.FileName);
                string fullFilename = string.Format("{0}_{1}{2}", filename, DateTime.Now.ToString("yyyyMMddHHmmssfff"), ext);
                var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), fullFilename);
                uploadFile.SaveAs(fileSavePath);
                HttpContext.Current.Response.Write(new ResultPic() { Code = 0, Filename = "/UploadedFiles/" + fullFilename }.ToJSON());
            }
            else
            {
                HttpContext.Current.Response.Write(new Result() { Code = 100, Data = null, Message = "发生错误" }.ToJSON());
            }
        }

        [WebMethod]
        public string update(string post)
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
        public string handsUp(string post)
        {
            HandsUpPostData postData = post.FromJsonTo<HandsUpPostData>();


            if (postData.Type == 1)
            {
                if (this.Lazy_HandsUpRecordBLL.InsertHandsUpRecord(new HandsUpRecord() {
                    UserId = postData.UserId,
                    PetitionId = postData.PetitionId
                }))
                {
                    return new Result() { Code = 0, Data = null }.ToJSON();
                }
                else
                {
                    return new Result() { Code = 100, Data = null, Message = "发生错误" }.ToJSON();
                }
            }
            else
            {
                if (this.Lazy_HandsUpRecordBLL.DeleteHandsUpRecord(new HandsUpRecord()
                {
                    UserId = postData.UserId,
                    PetitionId = postData.PetitionId
                }))
                {
                    return new Result() { Code = 0, Data = null }.ToJSON();
                }
                else
                {
                    return new Result() { Code = 100, Data = null, Message = "发生错误" }.ToJSON();
                }
            }
            
        }
    }

    public class Result
    {
        public int Code { get; set; }
        public List<ReturnPetitionList> Data { get; set; }
        public string Message { get; set; }
    }

    public class ResultPic
    {
        public int Code { get; set; }
        public string Filename { get; set; }
        public string Message { get; set; }
    }

    public class InsertPetitionPostData
    {
        public Petition petition { get; set; }
        public List<Pic> pics { get; set; }
    }

    public class SelectPetitionPostData
    {
        public int petitionId { get; set; }
    }

    public class PetitionList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Decimal CreateTime { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public string ToUserName { get; set; }
        public int Status { get; set; }
        public int? HandsUp { get; set; }
        public string ImageUrl { get; set; }
    }

    public class ReturnPetitionList
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Decimal CreateTime { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public string ToUserName { get; set; }
        public int Status { get; set; }
        public int? HandsUp { get; set; }
        public List<string> Pics { get; set; }
    }

    public class HandsUpPostData
    {
        // 1=举手；0=取消举手
        public int Type { get; set; }
        public string UserId { get; set; }
        public int PetitionId { get; set; }
    }


}

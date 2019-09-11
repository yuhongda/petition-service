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
                        HandsUpCount = p.HandsUpCount,
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
                        HandsUpCount = p.HandsUpCount,
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

            int newId = this.Lazy_PetitionBLL.InsertPetition(postData.petition, postData.pics);
            if (newId != -1)
            {
                return new ResultPost() { Code = 0, PetitionId = newId }.ToJSON();
            }
            else
            {
                return new ResultPost() { Code = 100, PetitionId = newId, Message = "系统错误" }.ToJSON();
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
            HandsUpRecord newItem = new HandsUpRecord()
            {
                UserId = postData.UserId,
                PetitionId = postData.PetitionId
            };

            if (postData.Type == 1)
            {
                if (!this.Lazy_HandsUpRecordBLL.CheckIsHandsUp(newItem))
                {
                    if (this.Lazy_HandsUpRecordBLL.InsertHandsUpRecord(newItem))
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
                    return new Result() { Code = 101, Data = null, Message = "已举手" }.ToJSON();
                }

            }
            else
            {
                if (this.Lazy_HandsUpRecordBLL.DeleteHandsUpRecord(newItem))
                {
                    return new Result() { Code = 0, Data = null }.ToJSON();
                }
                else
                {
                    return new Result() { Code = 100, Data = null, Message = "发生错误" }.ToJSON();
                }
            }
        }

        [WebMethod]
        public string getHandsUpRecordByUserId(string post)
        {
            GetHandsUpPostData postData = post.FromJsonTo<GetHandsUpPostData>();
            List<HandsUpRecord> handsUpList = this.Lazy_HandsUpRecordBLL.GetHandsUpRecordByUserId(postData.UserId).ToList<HandsUpRecord>();
            return new ResultHandsUpRecord() { Code = 0, Data = handsUpList }.ToJSON();
        }

        [WebMethod]
        public string getHandsUpRecordByPetitionId(string post)
        {
            GetHandsUpPostData postData = post.FromJsonTo<GetHandsUpPostData>();
            List<HandsUpRecord> handsUpList = this.Lazy_HandsUpRecordBLL.GetHandsUpRecordByPetitionId(postData.PetitionId).ToList<HandsUpRecord>();
            return new ResultHandsUpRecord() { Code = 0, Data = handsUpList }.ToJSON();
        }

        [WebMethod]
        public string getVerifyCode(string post)
        {
            GetVerifyCodePostData postData = post.FromJsonTo<GetVerifyCodePostData>();

            string url = "http://smsbj1.253.com/msg/send/json";
            string account = "N1973877";
            string password = "FecuDBUHAPb1a6";
            string msg = "【玛娜星球】 您的验证码是{0}";
            string json = "\"account\":\"{0}\",\"password\":\"{1}\",\"phone\":\"{2}\",\"report\":\"true\",\"msg\":\"{3}\"";
            var result = "";

            Random random = new Random();
            string code = random.Next(100000, 999999).ToString();

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            NetworkCredential auth = new NetworkCredential(account, password);
            httpWebRequest.Credentials = auth;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write("{"+string.Format(json, account, password, postData.Phone, string.Format(msg, code))+"}");
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Cookie verifyCodeCookie = new Cookie("__PhoneVerifyCode@HandsUp__", code);
            httpResponse.Cookies.Add(verifyCodeCookie);
            ResultPhoneVerifyCode resultPhoneVerifyCode;
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                result = streamReader.ReadToEnd();
                resultPhoneVerifyCode = result.FromJsonTo<ResultPhoneVerifyCode>();

            }

            return new ReturnPhoneVerifyCode() { Code = resultPhoneVerifyCode.code, Data = code, Message = resultPhoneVerifyCode.errorMsg }.ToJSON();
        }
    }

    public class Result
    {
        public int Code { get; set; }
        public List<ReturnPetitionList> Data { get; set; }
        public string Message { get; set; }
    }

    public class ResultHandsUpRecord
    {
        public int Code { get; set; }
        public List<HandsUpRecord> Data { get; set; }
        public string Message { get; set; }
    }

    public class ResultPost
    {
        public int Code { get; set; }
        public int PetitionId { get; set; }
        public string Message { get; set; }
    }

    public class ResultPic
    {
        public int Code { get; set; }
        public string Filename { get; set; }
        public string Message { get; set; }
    }

    public class ResultPhoneVerifyCode
    {
        public int code { get; set; }
        public string msgId { get; set; }
        public string time { get; set; }
        public string errorMsg { get; set; }
    }

    public class ReturnPhoneVerifyCode
    {
        public int Code { get; set; }
        public string Data { get; set; }
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
        public int? HandsUpCount { get; set; }
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
        public int? HandsUpCount { get; set; }
    }

    public class HandsUpPostData
    {
        // 1=举手；0=取消举手
        public int Type { get; set; }
        public string UserId { get; set; }
        public int PetitionId { get; set; }
    }

    public class GetHandsUpPostData
    {
        public string UserId { get; set; }
        public int PetitionId { get; set; }
    }

    public class GetVerifyCodePostData
    {
        public string Phone { get; set; }
    }
    



}

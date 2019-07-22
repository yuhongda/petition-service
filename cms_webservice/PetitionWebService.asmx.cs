using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
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

        [WebMethod]
        public string getPetitionList()
        {
            List<Petition> list = this.Lazy_PetitionBLL.getPetitionList().ToList<Petition>();
            return new Result() {
                Code = 0,
                Data = list
            }.ToJSON();
        }
    }

    public class Result
    {
        public int Code { get; set; }
        public List<Petition> Data { get; set; }
    }
}

using System.Web;

namespace cms_webservice
{
    public class Global : HttpApplication
    {
        protected void Application_Start()
        {
            //var context = HttpContext.Current;
            //var response = context.Response;

            //// enable CORS
            //response.AddHeader("Access-Control-Allow-Origin", "*");

            //if (context.Request.HttpMethod == "OPTIONS")
            //{
            //    response.AddHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
            //    response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
            //    response.End();
            //}
        }
    }
}

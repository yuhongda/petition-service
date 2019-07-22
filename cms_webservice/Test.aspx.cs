using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cms_webservice
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Session["Code"] = "1234";
            Response.Write(this.Session["__VerifyCode@CMS__"]);
        }
    }
}
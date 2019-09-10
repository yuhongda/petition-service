<%@ WebHandler Language="C#" Class="ImageHandler" %>

using System;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Web.SessionState;

public class ImageHandler : IHttpHandler, IRequiresSessionState
{
    
    public void ProcessRequest (HttpContext context) {
        context.Response.ContentType = "image/gif";
        Bitmap b = new Bitmap(200, 60);
        Graphics g = Graphics.FromImage(b);
        g.FillRectangle(new SolidBrush(Color.White), 0, 0, 200, 60);
        Font font = new Font(FontFamily.GenericSerif, 48, FontStyle.Bold, GraphicsUnit.Pixel);
        Random r = new Random();
        string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        string letter;
        StringBuilder s = new StringBuilder();
        for (int i = 0; i < 5; i++)
        {
            letter = letters.Substring(r.Next(0, letters.Length - 1), 1);
            s.Append(letter);
            g.DrawString(letter, font, new SolidBrush(Color.FromArgb(1, 67, 156)), i * 38, r.Next(0, 15));
        }
        b.Save(context.Response.OutputStream, ImageFormat.Gif);
        context.Session["__VerifyCode@CMS__"] = s.ToString();
        HttpCookie verifyCodeCookie = new HttpCookie("__VerifyCode@CMS__");
        DateTime now = DateTime.Now;
        verifyCodeCookie.Value = s.ToString();
        verifyCodeCookie.Expires = now.AddHours(1);
        context.Response.Cookies.Add(verifyCodeCookie);
        context.Response.End();
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}
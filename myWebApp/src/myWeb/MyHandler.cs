using System.Text;
using System.Web;

namespace myWeb
{
    public class MyHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            var requestUrl = context.Request.Url;
            var content = Encoding.UTF8.GetBytes($"Hello {requestUrl}");
            context.Request.ContentType = "text/plain";
            context.Response.OutputStream.Write(content, 0, content.Length);
        }

        public bool IsReusable => true;
    }
}
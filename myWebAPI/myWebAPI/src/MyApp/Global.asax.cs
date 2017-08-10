using System;
using System.Web.Http;

namespace MyApp
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            var httpConfiguration = GlobalConfiguration.Configuration;
            BootStrap.Initialize(httpConfiguration);
        }
    }
}
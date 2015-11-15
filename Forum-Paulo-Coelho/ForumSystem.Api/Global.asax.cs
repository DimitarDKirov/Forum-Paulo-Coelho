namespace ForumSystem.Api
{
    using System.Reflection;
    using System.Web.Http;

    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            DatabaseConfig.Initialize();
            AutoMapperConfig.RegisterMappings(Assembly.Load("ForumSystem.Api"));
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}

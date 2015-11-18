using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http;
using System.Reflection;

namespace ForumSystem.Api.Tests
{
    [TestClass]
    public class TestInit
    {
       [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load("ForumSystem.Api"));

            var config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
    }
}

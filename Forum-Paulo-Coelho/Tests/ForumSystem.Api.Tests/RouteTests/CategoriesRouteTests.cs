namespace ForumSystem.Api.Tests.RouteTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTested.WebApi;
    using ForumSystem.Api.Controllers;
    using System.Net.Http;

    [TestClass]
    public class CategoriesRouteTests
    {
        [TestMethod]
        public void GetShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("/api/categories")
                .To<CategoriesController>(c => c.Get());
        }

        [TestMethod]
        public void PostShouldMapToAdd()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/categories?name=test")
                .WithHttpMethod(HttpMethod.Post)
                .To<CategoriesController>(c => c.Add("test"));
        }

        [TestMethod]
        public void PutShouldRouteToUpdate()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/categories/1?name=test")
                .WithHttpMethod(HttpMethod.Put)
                .To<CategoriesController>(c => c.Update(1, "test"));
        }
    }
}

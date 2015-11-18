namespace ForumSystem.Api.Tests.RouteTests
{
    using MyTested.WebApi;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ForumSystem.Api.Controllers;
    using System.Net.Http;
    using ForumSystem.Api.Models.Threads;

    [TestClass]
    public class ThreadsRouteTests
    {
        [TestMethod]
        public void GetShouldRouteToGetAll()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/threads")
                .To<ThreadsController>(t => t.GetAll());
        }

        [TestMethod]
        public void GetWithIdShouldMapToGetById()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/threads/1")
                .To<ThreadsController>(t => t.GetById(1));
        }

        [TestMethod]
        public void PostShouldMapCorrectly()
        {
            var request = new ThreadRequestModel
            {
                Content = "test content",
                Title = "test title"
            };

            MyWebApi
                .Routes()
                .ShouldMap("api/threads")
                .WithHttpMethod(HttpMethod.Post)
                .WithJsonContent(@"{""content"":""test content"", ""title"":""test title""}")
                .To<ThreadsController>(t => t.Post(request));
        }

        [TestMethod]
        public void FindByCategoryShouldMapToGetByCategory()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/threads?categoryId=1")
                .To<ThreadsController>(t => t.GetByCategory(1));
        }
    }
}

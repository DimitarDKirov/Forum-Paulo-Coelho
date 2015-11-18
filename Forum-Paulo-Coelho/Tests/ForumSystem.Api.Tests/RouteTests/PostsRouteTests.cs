namespace ForumSystem.Api.Tests.RouteTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyTested.WebApi;
    using ForumSystem.Api.Controllers;
    using System.Net.Http;
    using ForumSystem.Api.Models.Posts;

    [TestClass]
    public class PostsRouteTests
    {
        [TestMethod]
        public void PostGetByIdShouldMapToCorrectRoute()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/posts/1")
                .To<PostsController>(p => p.Get(1));
        }

        [TestMethod]
        public void GetByThreadIdShouldMapCorrectly()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/posts?threadId=1")
                .To<PostsController>(p => p.GetByThread(1));
        }

        [TestMethod]
        public void GetByUserShouldMapCorreclty()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/posts")
                .To<PostsController>(p => p.GetByUser());
        }

        [TestMethod]
        public void PostShoulMapToAdd()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/posts?threadId=1")
                .WithHttpMethod(HttpMethod.Post)
                .WithJsonContent(@"{""content"":""test""}")
                .To<PostsController>(p => p.Add(1,
                    new PostsRequestModel
                    {
                        Content = "test"
                    }));
        }

        [TestMethod]
        public void PutShouldMapToUpdate()
        {
            MyWebApi
                .Routes()
                .ShouldMap("api/posts/1")
                .WithHttpMethod(HttpMethod.Put)
                .WithJsonContent(@"{""content"":""test""}")
                .To<PostsController>(p => p.Update(1,
                    new PostsRequestModel
                    {
                        Content = "test"
                    }));
        }
    }
}

namespace ForumSystem.Api.Tests.ControllersTests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ForumSystem.Services.Contracts;
    using ForumSystem.Api.Tests.TestObjects;
    using ForumSystem.Api.Controllers;
    using System.Web.Http.Results;
    using ForumSystem.Api.Models.Posts;

    [TestClass]
    public class PostsControllerTest
    {
        private IPostsService postsService;

        [TestInitialize]
        public void Init()
        {
            this.postsService = TestObjectsFactory.GetPostsService();
        }

        [TestMethod]
        public void SearchByIdShouldReturnOkWithData()
        {
            var controller = new PostsController(postsService);

            var result = controller.Get(1);
            var okResult = result as OkNegotiatedContentResult<PostsResponseModel>;
            Assert.IsNotNull(okResult, "The contorller should return OK");
            Assert.AreEqual(1, okResult.Content.Id, "Returned post should be with Id 1");
        }

        [TestMethod]
        public void SearchByThreadShouldReturnOkWithData()
        {
            var controller = new PostsController(postsService);

            var result = controller.GetByThread(1);
            var okResult = result as OkNegotiatedContentResult<List<PostsResponseModel>>;
            Assert.IsNotNull(okResult, "The contorller should return OK");
            Assert.AreEqual(1, okResult.Content.Count, "Returned post should be one");
        }

        [TestMethod]
        public void GetByUserShouldRetunOkWithData()
        {
            var controller = new PostsController(postsService);

            var result = controller.GetByUser();
            var okResult = result as OkNegotiatedContentResult<List<PostsResponseModel>>;
            Assert.IsNotNull(okResult, "The contorller should return OK");
            Assert.AreEqual(1, okResult.Content.Count, "Returned post should be one");
        }

        [TestMethod]
        public void AddWithInvalidThreadReturnBadRequest()
        {
            var validData = new PostsRequestModel
            {
                Content = "test post"
            };
            var controller = new PostsController(postsService);

            var result = controller.Add(120, validData);
            var badRequest = result as BadRequestErrorMessageResult;
            Assert.IsNotNull(badRequest, "Should return BadRequest");
        }

        [TestMethod]
        public void AddWithValidDataShouldReturnOk()
        {
            var validData = new PostsRequestModel
            {
                Content = "test post"
            };
            var controller = new PostsController(postsService);

            var result = controller.Add(1, validData);
            var okResult = result as OkNegotiatedContentResult<string>;
            Assert.IsNotNull(okResult, "Should return ok");
        }

        [TestMethod]
        public void UpdateWithValidDataShoulrReturnOk()
        {
            var validData = new PostsRequestModel
             {
                 Content = "test post"
             };
            var controller = new PostsController(postsService);

            var result = controller.Update(1, validData);
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult, "Should return ok");
        }
    }
}

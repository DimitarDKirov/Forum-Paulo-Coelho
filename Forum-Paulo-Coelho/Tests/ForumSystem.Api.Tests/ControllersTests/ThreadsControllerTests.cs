namespace ForumSystem.Api.Tests.ControllersTests
{
    using System.Collections.Generic;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Http.Results;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ForumSystem.Api;
    using ForumSystem.Api.Controllers;
    using ForumSystem.Api.Models.Threads;
    using ForumSystem.Services.Contracts;
    using ForumSystem.Api.Tests.TestObjects;

    [TestClass]
    public class ThreadsControllerTests
    {
        private IThreadService threadService;

        [TestInitialize]
        public void Init()
        {
            this.threadService = TestObjectsFactory.GetThreadService();
        }

        [TestMethod]
        public void GetAllShouldReturnAllThreads()
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load("ForumSystem.Api"));

            var controller = new ThreadsController(this.threadService);

            var allThreads = controller.GetAll();

            var okResult = allThreads as OkNegotiatedContentResult<List<ThreadResponseModel>>;

            Assert.IsNotNull(allThreads);
            Assert.AreEqual(3, okResult.Content.Count);
        }

        [TestMethod]
        public void GetByIdShouldReturnThreadWithCirrectId()
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load("ForumSystem.Api"));

            var controller = new ThreadsController(this.threadService);

            var thread = controller.GetById(1);

            var okResult = thread as OkNegotiatedContentResult<ThreadResponseModel>;

            Assert.IsNotNull(thread);
            Assert.AreEqual(1, okResult.Content.Id);
        }

        [TestMethod]
        public void PostShouldReturnCorrectThread()
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load("ForumSystem.Api"));

            var controller = new ThreadsController(this.threadService);

            var okResponse = controller.Post(new ThreadRequestModel
            {
                Title = "Title",
                Content = "Content"
            });

            var okResult = okResponse as OkNegotiatedContentResult<ThreadResponseModel>;

            Assert.IsNotNull(okResponse);
            Assert.AreEqual(2, okResult.Content.Id);
            Assert.AreEqual("Some Content", okResult.Content.Content);
            Assert.AreEqual("Test Title", okResult.Content.Title);
        }

        [TestMethod]
        public void PostShouldValidateModelState()
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load("ForumSystem.Api"));

            var controller = new ThreadsController(this.threadService);
            controller.Configuration = new HttpConfiguration();

            var model = new ThreadRequestModel
            {
                Content = "Content"
            };

            controller.Validate(model);
            var result = controller.Post(model);

            Assert.IsFalse(controller.ModelState.IsValid);
        }

        [TestMethod]
        public void GetByCategoryIdShouldReturnThreadsWithCategory()
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load("ForumSystem.Api"));

            var controller = new ThreadsController(this.threadService);

            var allThreads = controller.GetByCategory(2);

            var okResult = allThreads as OkNegotiatedContentResult<List<ThreadResponseModel>>;

            Assert.IsNotNull(allThreads);
            Assert.AreEqual(2, okResult.Content.Count);
        }
    }
}

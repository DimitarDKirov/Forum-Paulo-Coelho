namespace ForumSystem.Api.Tests.ControllersTests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ForumSystem.Services.Contracts;
    using ForumSystem.Api.Tests.TestObjects;
    using ForumSystem.Api.Controllers;
    using System.Web.Http.Results;
    using ForumSystem.Models;

    [TestClass]
    public class CategoriesControllerTests
    {
        private ICategoriesService categories;

        [TestInitialize]
        public void Init()
        {
            this.categories = TestObjectsFactory.GetCategoriesService();
        }

        [TestMethod]
        public void GetShouldReturnOk()
        {
            var controller = new CategoriesController(categories);

            var result = controller.Get();
            var okResult = result as OkNegotiatedContentResult<List<Category>>;
            Assert.IsNotNull(okResult, "Should return OK");
            Assert.AreEqual(1, okResult.Content.Count);
        }

        [TestMethod]
        public void AddShouldReturnOK()
        {
            var controller = new CategoriesController(categories);
            var result = controller.Add("test");
            var okResult = result as OkNegotiatedContentResult<int>;
            Assert.IsNotNull(okResult, "Add should return OK");
            Assert.AreEqual(2, okResult.Content);
        }

        [TestMethod]
        public void UpdateWithNotExistingCategoryShouldReturnBadRequest()
        {
            var controller = new CategoriesController(categories);
            var result = controller.Update(120, "fail");
            var badResult = result as BadRequestErrorMessageResult;
            Assert.IsNotNull(badResult, "Update with invalid id should return bad request");
        }

        [TestMethod]
        public void UpdateWithValidDataShouldReturnOk()
        {
            var controller = new CategoriesController(categories);
            var result = controller.Update(1, "new title");
            var okResult = result as OkResult;
            Assert.IsNotNull(okResult, "Update with valid id should return OK");
        }
    }
}

namespace ForumSystem.Api.Tests.ControllersTests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ForumSystem.Services.Contracts;
    using ForumSystem.Api.Tests.TestObjects;
    using ForumSystem.Api.Controllers;
    using System.Web.Http.Results;
    using ForumSystem.Api.Models;
    using ForumSystem.Models;
    using Telerik.JustMock;
    using System;
    using ForumSystem.Data;
    using System.Threading;
    using System.Web.Http;
    using System.Net.Http;
    using System.Web.Http.Routing;
    using System.Reflection;

    [TestClass]
    public class CommentsControllerTest
    {

        [TestInitialize]
        public void Init()
        {
            Comment[] comment = this.GenerateValidTestComments(0);
        }

        public void CreateShouldReturnCorrectComment()
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load("ForumSystem.Api"));

            var data = this.GenerateValidTestComments(1);

            var controller = new CommentsController(data);

            var okResponse = controller.Create(1, new CommentDataModel
            {
                Content = "Content"
            });

            var okResult = okResponse as OkNegotiatedContentResult<CommentDataModel>;

            Assert.IsNotNull(okResponse);
            Assert.AreEqual(2, okResult.Content.Id);
            Assert.AreEqual("Some Content", okResult.Content.Content);
        }

        [TestMethod]
        public void GetAll_WhenCommentsInDb_ShouldReturnComments()
        {
            Comment[] comments = this.GenerateValidTestComments(1);

            var repo = Mock.Create<IRepository<Comment>>();
            Mock.Arrange(() => repo.All())
                .Returns(() => comments.AsQueryable());

            var data = Mock.Create<IForumDbContext>();

            Mock.Arrange(() => data.Comments)
                .Returns(() => repo);

            var controller = new CommentsController(data);
           // this.SetupController(controller);

            var actionResult = controller.Create(1, new CommentDataModel);

            var response = actionResult.ExecuteAsync(CancellationToken.None).Result;

            var actual = response.Content.ReadAsAsync<IEnumerable<CommentDataModel>>().Result.Select(a => a.ID).ToList();

            var expected = comments.AsQueryable().Select(a => a.ID).ToList();

            CollectionAssert.AreEquivalent(expected, actual);
        }

        private Comment[] GenerateValidTestComments(int count)
        {
            Comment[] comments = new Comment[count];

            for (int i = 0; i < count; i++)
            {
                var comment = new Comment
                {
                    PostId = i,
                    Content = "The Content #" + i,
                    CommentDate = DateTime.Now,
                    User = new User()
                };
                comments[i] = comment;
            }

            return comments;
        }
    }
}

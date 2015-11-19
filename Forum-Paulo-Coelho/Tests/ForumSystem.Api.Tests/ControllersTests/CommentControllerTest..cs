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
    //using Telerik.JustMock;
    using Moq;
    using System;
    using System.Linq;
    using ForumSystem.Data;
    using System.Threading;
    using System.Web.Http;
    using System.Net.Http;
    using System.Web.Http.Routing;
    using System.Reflection;
    using System.Data.Entity;

    [TestClass]
    public class CommentsControllerTest
    {
        private IList<Comment> comments;

        [TestInitialize]
        public void Init()
        {
            comments = this.GenerateValidTestComments(10);
        }

        [TestMethod]
        public void CreateShouldReturnCorrectComment()
        {
            AutoMapperConfig.RegisterMappings(Assembly.Load("ForumSystem.Api"));
            var data = comments.AsQueryable();
            var mockSet = new Mock<DbSet<Comment>>();
            mockSet.As<IQueryable<Comment>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Comment>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Comment>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Comment>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<IForumDbContext>();
            mockContext.Setup(c => c.Comments).Returns(mockSet.Object);

            var controller = new CommentsController(mockContext.Object);

            var okResponse = controller.Create(1, new CommentDataModel
            {
                Id = 1,
                Content = "Content"
            });

            var okResult = okResponse as OkNegotiatedContentResult<CommentDataModel>;

            Assert.IsNotNull(okResponse);
            Assert.AreEqual(1, okResult.Content.Id);
            Assert.AreEqual("Content", okResult.Content.Content);
        }

        [TestMethod]
        public void GetAll_WhenCommentsInDb_ShouldReturnComments()
        {
            //Comment[] comments = this.GenerateValidTestComments(1);

           // var repo = Mock.Create<IRepository<Comment>>();
           // Mock.Arrange(() => repo.All())
           //     .Returns(() => comments.AsQueryable());

           // var data = Mock.Create<IForumDbContext>();

           // Mock.Arrange(() => data.Comments)
           //     .Returns(() => repo);

           // var controller = new CommentsController(data);
           //// this.SetupController(controller);

           // var actionResult = controller.Create(1, new CommentDataModel);

           // var response = actionResult.ExecuteAsync(CancellationToken.None).Result;

           // var actual = response.Content.ReadAsAsync<IEnumerable<CommentDataModel>>().Result.Select(a => a.ID).ToList();

           // var expected = comments.AsQueryable().Select(a => a.ID).ToList();

           // CollectionAssert.AreEquivalent(expected, actual);
        }

        private IList<Comment> GenerateValidTestComments(int count)
        {
            List<Comment> comments = new List<Comment>();

            for (int i = 0; i < count; i++)
            {
                var comment = new Comment
                {
                    PostId = i,
                    Content = "The Content #" + i,
                    CommentDate = DateTime.Now,
                    User = new User()
                };
                comments.Add(comment);
            }

            return comments;
        }
    }
}

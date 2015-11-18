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

    public class CommentsControllerTest
    {

        [TestClass]
        public class ArticlesControllerTest
        {
            private Comment comment;

            [TestMethod]
            public void GetAll_WhenCommentsInDb_ShouldReturnComments()
            {
                this.comment = TestObjectsFactory.GetCommentsRepository();

               
            }
        }
    }
}
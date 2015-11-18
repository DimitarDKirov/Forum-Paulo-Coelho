namespace ForumSystem.Api.Tests.ControllersTests
{
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ForumSystem.Services.Contracts;
    using ForumSystem.Api.Tests.TestObjects;
    using ForumSystem.Api.Controllers;
    using System.Web.Http.Results;
    using ForumSystem.Api.Models;

    public class CommentsControllerTest
    {
        [TestInitialize]
        public void Init()
        {
            this.postsService = TestObjectsFactory.GetPostsService();
        }
    }
}

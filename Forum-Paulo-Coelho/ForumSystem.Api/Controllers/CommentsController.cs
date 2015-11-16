namespace ForumSystem.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using Microsoft.AspNet.Identity;
    using ForumSystem.Api.Models;
    using ForumSystem.Data;
    using ForumSystem.Models;

    public class CommentsController : BaseApiController
    {
        public CommentsController(IForumDbContext data)
            : base(data)
        {
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Create(int id, CommentDataModel model)
        {
            var userID = this.User.Identity.GetUserId();
            var comment = new Comment
            {
                PostId = id,
                UserId = userID,
                Content = model.Content,
                CommentDate = DateTime.Now
            };

            this.data.Comments.Add(comment);
            this.data.SaveChanges();

            return Ok();
        }
    }
}
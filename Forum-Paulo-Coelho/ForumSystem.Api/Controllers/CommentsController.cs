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
    using System.Web.Http.Cors;

    [EnableCors("*", "*", "*")]
    public class CommentsController : BaseApiController
    {
        public CommentsController(IForumDbContext data)
            : base(data)
        {
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Create(int postId, CommentDataModel model)
        {
            var userID = this.User.Identity.GetUserId();
            var comment = new Comment
            {
                PostId = postId,
                UserId = userID,
                Content = model.Content,
                CommentDate = DateTime.Now
            };

            this.data.Comments.Add(comment);
            try
            {
                this.data.SaveChanges();
            }
            catch
            {
                return this.BadRequest();
            }

            return Ok(model);
        }
    }
}
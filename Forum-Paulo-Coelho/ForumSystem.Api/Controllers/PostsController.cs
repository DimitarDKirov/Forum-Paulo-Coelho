using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ForumSystem.Data;
using ForumSystem.Models;

namespace ForumSystem.Api.Controllers
{
    public class PostsController:ApiController
    {
        private IRepository<Post> postsRepo;

        public PostsController(IRepository<Post> posts)
        {
            this.postsRepo = posts;
        }

        public IHttpActionResult Get(int postId)
        {
            var post = this.postsRepo
                .GetById(postId);

            return this.Ok(post);
        }

        public IHttpActionResult GetByThread(int threadId)
        {
            var posts = this.postsRepo
                .All()
                .Where(p => p.ThreadId == threadId)
                .ToList();

            return this.Ok(posts);
        }
    }
}
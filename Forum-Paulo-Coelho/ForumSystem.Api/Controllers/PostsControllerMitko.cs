using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ForumSystem.Data;
using ForumSystem.Models;
using ForumSystem.Services.Contracts;
using ForumSystem.Api.Models.Posts;
using AutoMapper.QueryableExtensions;

namespace ForumSystem.Api.Controllers
{
    public class PostsControllerMitko:ApiController
    {
        private IPostsService postsService;

        public PostsControllerMitko(IPostsService service)
        {
            this.postsService = service;
        }

        [HttpGet]
        public IHttpActionResult Get(int postId)
        {
            var post = this.postsService
                .GetById(postId);

            return this.Ok(post);
        }

        [HttpGet]
        [Route("api/posts/bythread/{threadId}")]
        public IHttpActionResult GetByThread(int threadId)
        {
            var posts = this.postsService
                .GetByThread(threadId)
                .ProjectTo<PostsResponseModel>()
                .ToList();

            return this.Ok(posts);
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetByUser()
        {
            var user = this.User.Identity.Name;

            var posts=this.postsService
                .GetByUser(user)
                .ProjectTo<PostsResponseModel>()
                .ToList();

            return this.Ok(posts);
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult Add(PostsRequestModel post)
        {
            if(!this.ModelState.IsValid)
            {
                return this.BadRequest("Incorrect post data");
            }

            string userName= this.User.Identity.Name;
            int postId = this.postsService
                .Add(post.Content, post.ThreadId, userName);

            return this.Ok();
        }

        [Authorize]
        [HttpPut]
        public IHttpActionResult Update(int postId, PostsRequestModel post)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest("Incorrect post data");
            }

            try
            {
                this.postsService.Update(postId, post.Content);
            }
            catch(ArgumentException ae)
            {
                return this.BadRequest(ae.Message);
            }

            return this.Ok();
        }
    }
}
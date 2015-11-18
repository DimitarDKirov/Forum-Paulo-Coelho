namespace ForumSystem.Api.Controllers
 {
     using System;
     using System.Collections.Generic;
     using System.Linq;
     using System.Web;
     using System.Web.Http;
     using ForumSystem.Data;
     using ForumSystem.Models;
     using ForumSystem.Api.Models.Posts;
     //using io.iron.ironmq;
     using IronMQ;
     using ForumSystem.Services.Contracts;
     using AutoMapper.QueryableExtensions;
     using AutoMapper;

     public class PostsController : ApiController
     {
         private IPostsService postsService;

         public PostsController(IPostsService service)
         {
             this.postsService = service;
         }

         //api/posts/1
         [HttpGet]
         public IHttpActionResult Get(int id)
         {
             var post = this.postsService
                 .GetById(id);

             if (post == null)
             {
                 return this.NotFound();
             }

             var responsePost = Mapper.Map<PostsResponseModel>(post);

             return this.Ok(responsePost);
         }

         //api/posts?threadId=1
         [HttpGet]
         public IHttpActionResult GetByThread(int threadId)
         {
             var posts = this.postsService
                 .GetByThread(threadId)
                 .ProjectTo<PostsResponseModel>()
                 .ToList();

             return this.Ok(posts);
         }

         //api/posts
         [Authorize]
         [HttpGet]
         public IHttpActionResult GetByUser()
         {
             var user = this.User.Identity.Name;

             var posts = this.postsService
                 .GetByUser(user)
                 .ProjectTo<PostsResponseModel>()
                 .ToList();

             return this.Ok(posts);
         }

         //api/posts?threadId=1
         [Authorize]
         [HttpPost]
         public IHttpActionResult Add(int threadId, PostsRequestModel post)
         {
             if (!this.ModelState.IsValid)
             {
                 return this.BadRequest("Incorrect post data");
             }

             string userName = this.User.Identity.Name;
             int postId;
             try
             {
                 postId = this.postsService
                      .Add(post.Content, threadId, userName);
             }
             catch (ArgumentException ae)
             {
                 return this.BadRequest(ae.Message);
             }

             return this.Ok("Post " + postId + " added");
         }

         //api/posts/1
         [Authorize]
         [HttpPut]
         public IHttpActionResult Update(int id, PostsRequestModel post)
         {
             if (!this.ModelState.IsValid)
             {
                 return this.BadRequest("Incorrect post data");
             }

             try
             {
                 this.postsService.Update(id, post.Content);
             }
             catch (ArgumentException ae)
             {
                 return this.BadRequest(ae.Message);
             }

             return this.Ok();
         }
     }
 }
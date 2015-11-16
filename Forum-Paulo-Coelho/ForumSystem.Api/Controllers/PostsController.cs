using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ForumSystem.Data;
using ForumSystem.Models;
using ForumSystem.Api.Models.Posts;
using io.iron.ironmq;

namespace ForumSystem.Api.Controllers
{
    public class PostsController:ApiController
    {
        private const string MessageQueueProjectId = "5649969d4aa03100090000b2";
        private const string MessageQueueToken = "j46Yol8vc3puwszWc9O3";
        private IRepository<Post> postsRepo;
        private IRepository<Thread> threadsRepo;
        private IRepository<User> usersRepo;

        public PostsController(IRepository<Post> posts, IRepository<Thread> thread, IRepository<User> users)
        {
            this.postsRepo = posts;
            this.threadsRepo = thread;
            this.usersRepo = users;
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

        [Authorize]
        [HttpPost]
        public IHttpActionResult Create(int id, PostCreateRequestModel postModel)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            var dbThread = this.threadsRepo
                .All()
                .FirstOrDefault(t => t.Id == id);

            if (dbThread == null)
            {
                return BadRequest("Thread with that id does not exist");
            }

            var currentUser = this.usersRepo
                .All()
                .FirstOrDefault(u => u.UserName == this.User.Identity.Name);

            var post = new Post
            {
                Content = postModel.Content,
                PostDate = DateTime.Now
            };

            var otherUser = this.threadsRepo
                .All()
                .FirstOrDefault(t => t.Id == dbThread.Id)
                .User;

            otherUser.Notifications.Add(new Notification
            {
                Message = this.User.Identity.Name + " add post on your thread.",
                DateCreated = DateTime.Now
            });

            this.usersRepo.SaveChanges();

			// Implement notifications functionality or message queues
            Client client = new Client(MessageQueueProjectId, MessageQueueToken);
            Queue queue = client.queue(otherUser.Nickname);
            queue.push("[" + this.User.Identity.Name + "]" + " add post on your thread.");

            currentUser.Posts.Add(post);
            dbThread.Posts.Add(post);
            threadsRepo.SaveChanges();

            return Ok(postModel);
        }
    }
}
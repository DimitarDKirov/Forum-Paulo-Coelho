namespace ForumSystem.Api.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using ForumSystem.Data;
    using ForumSystem.Models;
    using Models.Threads;

    public class ThreadsController : ApiController
    {
        private IRepository<Thread> threads;
        private IRepository<User> users;

        public ThreadsController(IRepository<Thread> threads, IRepository<User> users)
        {
            this.threads = threads;
            this.users = users;
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var threads = this.threads
                .All()
                .ProjectTo<ThreadResponseModel>()
                .ToList();

            return this.Ok(threads);
        }

        [HttpGet]
        public IHttpActionResult GetById(int id)
        {
            var thread = this.threads
                .All()
                .Where(t => t.Id == id)
                .ProjectTo<ThreadResponseModel>()
                .FirstOrDefault();

            if (thread == null)
            {
                return BadRequest("No thread with provided id");
            }

            return Ok(thread);
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult Post(ThreadRequestModel requestThread)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(this.ModelState);
            }

            var currentUser = this.users
                 .All()
                 .FirstOrDefault(u => u.UserName == this.User.Identity.Name);

            var dbThread = new Thread
            {
                Title = requestThread.Title,
                Content = requestThread.Content,
                UserId = currentUser.Id,
                DateCreated = DateTime.Now
            };

            this.threads.Add(dbThread);
            this.threads.SaveChanges();

            return Ok(requestThread);
        }

        [HttpGet]
        public IHttpActionResult GetByCategory(int categoryId)
        {
            var threads = this.threads
                .All()
                .Where(t => t.Categories.Any(c => c.Id == categoryId))
                .ToList();

            return this.Ok(threads);
        }

        //TODO update
    }
}

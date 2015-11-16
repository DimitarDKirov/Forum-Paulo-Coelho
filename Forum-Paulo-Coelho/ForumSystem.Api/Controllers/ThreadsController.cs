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
    using ForumSystem.Services.Contracts;

    public class ThreadsController : ApiController
    {
        private IThreadService threads;

        public ThreadsController(IThreadService thredsService)
        {
            this.threads = thredsService;
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

            this.threads.Add(
                requestThread.Title,
                requestThread.Content,
                this.User.Identity.Name);

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
    }
}

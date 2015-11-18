namespace ForumSystem.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using AutoMapper.QueryableExtensions;
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

            var dbThread = this.threads.Add(
                requestThread.Title,
                requestThread.Content,
                this.User.Identity.Name);

            var response = new ThreadResponseModel
            {
                Id = dbThread.Id,
                Title = dbThread.Title,
                Content = dbThread.Content,
                DateCreated = dbThread.DateCreated,
                Creator = this.User.Identity.Name
            };

            return Ok(response);
        }

        [HttpGet]
        public IHttpActionResult GetByCategory(int categoryId)
        {
            var threads = this.threads
                .All()
                .Where(t => t.Categories.Any(c => c.Id == categoryId))
                .ProjectTo<ThreadResponseModel>()
                .ToList();

            return this.Ok(threads);
        }
    }
}

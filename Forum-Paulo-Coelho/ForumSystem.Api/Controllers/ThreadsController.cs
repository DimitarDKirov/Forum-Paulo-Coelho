namespace ForumSystem.Api.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using ForumSystem.Data;
    using ForumSystem.Models;

    public class ThreadsController : ApiController
    {
        private IRepository<Thread> threads;

        public ThreadsController()
            : this(new EfGenericRepository<Thread>(new ForumDbContext()))
        {
        }

        public ThreadsController(IRepository<Thread> threads)
        {
            this.threads = threads;
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var threads = this.threads.All().ToList();

            return this.Ok(threads);
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

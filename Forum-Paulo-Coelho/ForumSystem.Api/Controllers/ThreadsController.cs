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
            :this(new EfGenericRepository<Thread>(new ForumDbContext()))
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

            return Ok(threads);
        }
    }
}

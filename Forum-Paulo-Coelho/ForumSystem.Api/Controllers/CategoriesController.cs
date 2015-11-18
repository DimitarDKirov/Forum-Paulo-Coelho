namespace ForumSystem.Api.Controllers
{
    using ForumSystem.Services.Contracts;
    using System;
    using System.Linq;
    using System.Web.Http;

    public class CategoriesController : ApiController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IHttpActionResult Get()
        {
            var categories = this.categoriesService.GetAll();
            return this.Ok(categories.ToList());
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult Add(string name)
        {
            var id = this.categoriesService.Add(name);
            return this.Ok(id);
        }

        [Authorize]
        [HttpPut]
        public IHttpActionResult Update(int id, string name)
        {
            try
            {
                this.categoriesService.Update(id, name);
            }
            catch (ArgumentException ae)
            {
                return this.BadRequest(ae.Message);
            }

            return this.Ok();
        }
    }
}
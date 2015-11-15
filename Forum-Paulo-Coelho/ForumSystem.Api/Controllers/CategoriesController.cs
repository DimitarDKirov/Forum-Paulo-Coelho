using ForumSystem.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace ForumSystem.Api.Controllers
{
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
        public IHttpActionResult Post(string name)
        {
            this.categoriesService.Add(name);
            return this.Ok();
        }

        [Authorize]
        public IHttpActionResult Put(int id, string name)
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
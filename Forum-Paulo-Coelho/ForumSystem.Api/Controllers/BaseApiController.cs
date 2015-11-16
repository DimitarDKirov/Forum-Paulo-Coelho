﻿namespace ForumSystem.Api.Controllers
{
    using ForumSystem.Data;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class BaseApiController : ApiController
    {
        protected IForumDbContext data;

        public BaseApiController(IForumDbContext data)
        {
            this.data = data;
        }
    }
}
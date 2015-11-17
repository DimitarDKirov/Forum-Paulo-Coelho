namespace ForumSystem.Api.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class LoggedUserModel
    {
        public string SessionKey { get; set; }

        public string Nickname { get; set; }
    }
}
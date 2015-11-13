namespace ForumSystem.Data
{
    using System.Data.Entity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using ForumSystem.Models;

    public class ForumDbContext : IdentityDbContext<User>, IForumDbContext
    {
        public ForumDbContext()
            : base("ForumSystemConnection", throwIfV1Schema: false)
        {
        }

        public virtual IDbSet<Thread> Threads { get; set; }

        public virtual IDbSet<Post> Posts { get; set; }

        public virtual IDbSet<Comment> Comments { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Vote> Votes { get; set; }

        public virtual IDbSet<Notification> Notifications { get; set; }

        public static ForumDbContext Create()
        {
            return new ForumDbContext();
        }
    }
}

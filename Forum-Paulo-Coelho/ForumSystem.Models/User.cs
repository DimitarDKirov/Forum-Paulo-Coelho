namespace ForumSystem.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    public class User : IdentityUser
    {
        private ICollection<Thread> threads;
        private ICollection<Post> posts;
        private ICollection<Vote> votes;
        private ICollection<Comment> comments;
        private ICollection<Notification> notifications;

        public User()
        {
            this.Threads = new HashSet<Thread>();
            this.Posts = new HashSet<Post>();
            this.Votes = new HashSet<Vote>();
            this.Comments = new HashSet<Comment>();
            this.Notifications = new HashSet<Notification>();
        }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Nickname { get; set; }

        [Required]
        public string AuthCode { get; set; }

        public string SessionKey { get; set; }

        public virtual ICollection<Thread> Threads
        {
            get { return this.threads; }
            set { this.threads = value; }
        }

        public virtual ICollection<Post> Posts
        {
            get { return this.posts; }
            set { this.posts = value; }
        }

        public virtual ICollection<Vote> Votes
        {
            get { return this.votes; }
            set { this.votes = value; }
        }

        public virtual ICollection<Comment> Comments
        {
            get { return this.comments; }
            set { this.comments = value; }
        }

        public virtual ICollection<Notification> Notifications
        {
            get { return this.notifications; }
            set { this.notifications = value; }
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}

namespace ForumSystem.Services
{
    using System;
    using System.Linq;
    using ForumSystem.Services.Contracts;
    using ForumSystem.Models;
    using ForumSystem.Data;

    public class ThreadService : IThreadService
    {
        private readonly IRepository<Thread> threads;
        private readonly IRepository<User> users;

        public ThreadService(IRepository<Thread> threads, IRepository<User> users)
        {
            this.threads = threads;
            this.users = users;
        }

        public IQueryable<Thread> All()
        {
            var threads = this.threads
               .All();

            return threads;
        }

        public Thread Add(string title, string content, string creator)
        {
            var currentUser = this.users
                 .All()
                 .FirstOrDefault(u => u.UserName == creator);

            if(currentUser == null)
            {
                throw new ArgumentNullException("No current login user");
            }

            var dbThread = new Thread
            {
                Title = title,
                Content = content,
                UserId = currentUser.Id,
                DateCreated = DateTime.Now
            };

            this.threads.Add(dbThread);
            this.threads.SaveChanges();

            return dbThread;
        }
    }
}

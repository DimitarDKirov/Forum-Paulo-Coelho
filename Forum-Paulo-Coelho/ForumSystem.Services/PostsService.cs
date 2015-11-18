using ForumSystem.Services.Contracts;
using ForumSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumSystem.Data;
using IronMQ;

namespace ForumSystem.Services
{
    public class PostsService : IPostsService
    {
        private const string MessageQueueProjectId = "5649969d4aa03100090000b2";
        private const string MessageQueueToken = "j46Yol8vc3puwszWc9O3";
        private readonly IRepository<Post> postsRepository;
        private readonly IRepository<User> usersRepository;
        private readonly IRepository<Thread> threadsRepository;

        public PostsService(IRepository<Post> postsRepo, IRepository<User> usersRepo, IRepository<Thread> threadsRepo)
        {
            this.postsRepository = postsRepo;
            this.usersRepository = usersRepo;
            this.threadsRepository = threadsRepo;
        }

        public Post GetById(int postId)
        {
            return this.postsRepository.GetById(postId);
        }

        public IQueryable<Post> GetByThread(int threadId)
        {
            var posts = this.postsRepository
                .All()
                .Where(p => p.ThreadId == threadId);

            return posts;
        }

        public IQueryable<Post> GetByUser(string username)
        {
            var user = this.FindUser(username);

            var posts = this.postsRepository
                .All()
                .Where(p => p.UserId == user.Id);

            return posts;
        }

        public int Add(string content, int threadId, string username)
        {
            var thread = this.threadsRepository.GetById(threadId);
            if (thread == null)
            {
                throw new ArgumentException("Thread not found");
            }

            var user = this.FindUser(username);
            var newPost = new Post
            {
                Content = content,
                PostDate = DateTime.Now,
                UserId = user.Id,
                ThreadId = threadId
            };

            this.postsRepository.Add(newPost);
            this.postsRepository.SaveChanges();

            var threadCreator = thread.User;
            if (user.Id != threadCreator.Id)
            {
                var threadUserNotification = new Notification
                {
                    Message = user.Nickname + " added post on your thread.",
                    DateCreated = newPost.PostDate
                };

                threadCreator.Notifications.Add(threadUserNotification);
                this.usersRepository.SaveChanges();

                // Implement notifications functionality or message queues
                Client client = new Client(MessageQueueProjectId, MessageQueueToken);
                Queue queue = client.Queue("Forum");
                queue.Push(user.Nickname + " add post on your thread.");
            }

            return newPost.Id;
        }

        public void Update(int postId, string content)
        {
            var post = this.GetById(postId);
            if (post == null)
            {
                throw new ArgumentException("Post not found");
            }

            post.Content = content;
            this.postsRepository.Update(post);
            this.postsRepository.SaveChanges();
        }

        private User FindUser(string username)
        {
            var user = this.usersRepository
                .All()
                .FirstOrDefault(u => u.UserName == username);

            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            return user;
        }

    }
}

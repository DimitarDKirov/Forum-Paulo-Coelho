using ForumSystem.Services.Contracts;
using ForumSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ForumSystem.Data;

namespace ForumSystem.Services
{
    public class PostsService : IPostsService
    {
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
            var user = this.FindUser(username);
            if (this.threadsRepository.GetById(threadId) == null)
            {
                throw new ArgumentException("Thred not found");
            }

            var newPost = new Post
            {
                Content = content,
                PostDate = DateTime.Now,
                UserId = user.Id,
                ThreadId = threadId
            };

            this.postsRepository.Add(newPost);
            this.postsRepository.SaveChanges();
            return newPost.Id;
        }

        public void Update(int postId, string Content)
        {
            var post = this.GetById(postId);
            if (post == null)
            {
                throw new ArgumentException("Post not found");
            }

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

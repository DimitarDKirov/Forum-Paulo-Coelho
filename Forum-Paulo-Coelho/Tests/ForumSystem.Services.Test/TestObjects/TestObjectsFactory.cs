using ForumSystem.Models;
using ForumSystem.Services.Contracts;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Services.Test.TestObjects
{
    public static class TestObjectsFactory
    {
        public static MemoryRepository<User> GetUsersRepository()
        {
            var usersRepository = new MemoryRepository<User>();
            for (int i = 0; i < 10; i++)
            {
                var user = new User
                {
                    Nickname = "Nick " + i,
                    Id = "id" + i,
                    UserName = "User" + i
                };

                usersRepository.Add(user);
            }

            return usersRepository;
        }

        public static MemoryRepository<Post> GetPostsRepository()
        {
            var postsRepository = new MemoryRepository<Post>();
            var users = GetUsersRepository();
            var threads = GetThreadsRepository();

            for (int i = 0; i < 20; i++)
            {
                var thread = threads.GetById(i % 5);
                var user = users.GetById("id" + i % 10);
                var post = new Post
                {
                    Content = "content" + i,
                    Id = i,
                    PostDate = new DateTime(2015, 11, i + 1),
                    ThreadId = thread.Id,
                    Thread = thread,
                    UserId = user.Id,
                    User = user
                };

                postsRepository.Add(post);
            }

            return postsRepository;
        }

        public static MemoryRepository<Thread> GetThreadsRepository()
        {
            var threadsRepository = new MemoryRepository<Thread>();
            var users = GetUsersRepository();

            for (int i = 0; i < 5; i++)
            {
                var user = users.GetById("id" + i % 10);
                var thread = new Thread
                {
                    Id = i,
                    Content = "content" + i,
                    DateCreated = new DateTime(2015, 11, i + 1),
                    Title = "title" + i,
                    UserId = user.Id,
                    User = user
                };

                threadsRepository.Add(thread);
            }

            return threadsRepository;
        }

        public static MemoryRepository<Comment> GetCommentsRepository()
        {
            var commentsRepository = new MemoryRepository<Comment>();
            var users = GetUsersRepository();
            var posts = GetPostsRepository();

            for (int i = 0; i < 20; i++)
            {
                var user = users.GetById("id" + i % 10);
                var post = posts.GetById(i);
                var comment = new Comment
                {
                    Id = i,
                    CommentDate = new DateTime(2015, 11, i + 1),
                    Content = "content" + i,
                    PostId = post.Id,
                    Post = post,
                    UserId = user.Id,
                    User = user,
                };

                commentsRepository.Add(comment);
            }

            return commentsRepository;
        }

        public static MemoryRepository<Category> GetCategoriesRepository()
        {
            var categoriesRepository = new MemoryRepository<Category>();
            for (int i = 0; i < 10; i++)
            {
                var category = new Category
                {
                    Id = i,
                    Name = "category" + i
                };

                categoriesRepository.Add(category);
            }

            return categoriesRepository;
        }
    }
}

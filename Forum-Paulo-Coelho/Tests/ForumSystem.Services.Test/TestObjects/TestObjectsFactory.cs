using ForumSystem.Models;
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
            for (int i = 0; i < 20; i++)
            {
                var post = new Post
                {
                    Content = "content" + i,
                    Id = i,
                    PostDate = new DateTime(2015, 11, i + 1),
                    ThreadId = i % 5,
                    UserId = "id" + i % 10
                };

                postsRepository.Add(post);
            }

            return postsRepository;
        }

        public static MemoryRepository<Thread> GetThreadsRepository()
        {
            var threadsRepository = new MemoryRepository<Thread>();
            for (int i = 0; i < 5; i++)
            {
                var thread = new Thread
                {
                    Id = i,
                    Content = "content" + i,
                    DateCreated = new DateTime(2015, 11, i + 1),
                    Title = "title" + i,
                    UserId = "id" + i % 10,
                };

                threadsRepository.Add(thread);
            }

            return threadsRepository;
        }

        public static MemoryRepository<Comment> GetCommentsRepository()
        {
            var commentsRepository = new MemoryRepository<Comment>();
            for (int i = 0; i < 20; i++)
            {
                var comment = new Comment
                {
                    Id = i,
                    CommentDate = new DateTime(2015, 11, i + 1),
                    Content = "content" + i,
                    PostId = i,
                    UserId = "id" + i % 10,
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

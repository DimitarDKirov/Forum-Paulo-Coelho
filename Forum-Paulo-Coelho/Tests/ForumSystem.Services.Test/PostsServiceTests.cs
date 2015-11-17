using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumSystem.Models;
using ForumSystem.Services.Test.TestObjects;
using System.Linq;

namespace ForumSystem.Services.Test
{
    [TestClass]
    public class PostsServiceTests
    {
        private PostsService service;
        private MemoryRepository<User> usersRepository;
        private MemoryRepository<Thread> threadsRepository;
        private MemoryRepository<Post> postsRepository;

        [TestInitialize]
        public void Init()
        {
            usersRepository = TestObjectsFactory.GetUsersRepository();
            threadsRepository = TestObjectsFactory.GetThreadsRepository();
            postsRepository = TestObjectsFactory.GetPostsRepository();
            this.service = new PostsService(postsRepository, usersRepository, threadsRepository);
        }

        [TestMethod]
        public void SearchByThreadIdShouldFindCorrectResult()
        {
            var result = this.service.GetByThread(3);
            Assert.AreEqual(4, result.ToList().Count, "Search by thread shoud return the correct element");
        }
    }
}

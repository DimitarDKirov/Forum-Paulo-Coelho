namespace ForumSystem.Services.Test
{
    using System;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ForumSystem.Data;
    using ForumSystem.Models;
    using ForumSystem.Services.Contracts;
    using ForumSystem.Services.Test.TestObjects;

    [TestClass]
    public class ThreadsServiceTests
    {
        private IRepository<Thread> threads;
        private IRepository<User> users;
        private IThreadService threadsService;

        [TestInitialize]
        public void Init()
        {
            this.users = TestObjectsFactory.GetUsersRepository();
            this.threads = TestObjectsFactory.GetThreadsRepository();
            this.threadsService = new ThreadService(this.threads, this.users);
        }

        [TestMethod]
        public void AllShouldReturnThreads()
        {
            var threads = this.threadsService.All();

            Assert.IsNotNull(threads);
            Assert.AreEqual(5, threads.Count());
        }

        [TestMethod]
        public void AddShouldReturnAddedThread()
        {
            var thread = this.threadsService.Add("Title from test", "test content", "User1");

            Assert.IsNotNull(thread);
            Assert.AreEqual("Title from test", thread.Title);
            Assert.AreEqual("test content", thread.Content);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddWithInvalidUsernameShouldThrow()
        {
            var thread = this.threadsService.Add("Title from test", "test content", "InvalidUserName");
        }
    }
}

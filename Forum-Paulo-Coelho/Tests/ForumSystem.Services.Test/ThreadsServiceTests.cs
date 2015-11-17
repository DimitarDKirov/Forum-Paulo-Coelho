using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumSystem.Data;
using ForumSystem.Models;
using ForumSystem.Services.Contracts;
using ForumSystem.Services.Test.TestObjects;

namespace ForumSystem.Services.Test
{
    [TestClass]
    public class ThreadsServiceTests
    {
        private IRepository<Thread> threads;
        private IRepository<User> users;
        private IThreadService service;

        [TestInitialize]
        public void Init()
        {
            this.users = TestObjectsFactory.GetUserRepository();
           // this.service = new ThreadService()
        }
    }
}

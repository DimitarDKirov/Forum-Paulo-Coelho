namespace ForumSystem.Services.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ForumSystem.Models;
    using ForumSystem.Services.Test.TestObjects;
    using System.Linq;

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

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Expected Argument Exception if user does not exist")]
        public void SearchByNotExistingUserShouldThrow()
        {
            int savesBefore = this.postsRepository.NumberOfSaves;
            var result = this.service.GetByUser("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Expected Argument Exception if user does not exist")]
        public void AddWithNotExistingUserShouldThrow()
        {
            int savesBefore = this.postsRepository.NumberOfSaves;
            this.service.Add("some content", 1, "");
            Assert.AreEqual(0, this.postsRepository.NumberOfSaves - savesBefore, "Wrong data should not invoke SaveChanges");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Expected Argument Exception if thread does not exist")]
        public void AddWithNotExistingThreadShouldThrow()
        {
            var postUser = this.usersRepository.All().FirstOrDefault();
            int savesBefore = this.postsRepository.NumberOfSaves;
            this.service.Add("some content", 12312321, postUser.UserName);
            Assert.AreEqual(0, this.postsRepository.NumberOfSaves - savesBefore, "Wrong data should not invoke SaveChanges");
        }

        [TestMethod]
        public void AddWithCorrectDataShouldInvokeSaveChangesAndStoreCorrectData()
        {
            int savesBefore = this.postsRepository.NumberOfSaves;
            var postToAdd = this.postsRepository.All().FirstOrDefault();
            var postUser = this.usersRepository.All().LastOrDefault();
            int addedPostId = this.service.Add(postToAdd.Content, postToAdd.ThreadId, postUser.UserName);
            Assert.AreEqual(1, this.postsRepository.NumberOfSaves - savesBefore, "Data should invoke Save Changes");
            var addedPost = this.postsRepository.GetById(addedPostId);
            Assert.IsNotNull(addedPost, "Post should be added");
            Assert.AreEqual(postToAdd.Content, addedPost.Content, "Added post should contain correct content");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Update of not existing post should throw")]
        public void UpdateWithNotExistingPostShouldThrow()
        {
            this.service.Update(113123, "");
        }

        [TestMethod]
        public void UpdateShouldSaveChangesAndUpdateContent()
        {
            int savesBefore = this.postsRepository.NumberOfSaves;
            var postToUpdate = this.postsRepository
                .All()
                .FirstOrDefault();
            string newContent = "Updated content";
            this.service.Update(postToUpdate.Id, "Updated content");
            Assert.AreEqual(1, this.postsRepository.NumberOfSaves - savesBefore, "Update should invoke save changes");
            Assert.AreEqual(newContent, this.postsRepository.GetById(postToUpdate.Id).Content, "Update should save new content");
        }
    }
}

using ForumSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumSystem.Services.Contracts
{
    public interface IPostsService
    {
        Post GetById(int id);

        IQueryable<Post> GetByThread(int threadId);

        IQueryable<Post> GetByUser(string userName);

        int Add(string content, int threadId, string userName);

        void Update(int postId, string Content);
    }
}

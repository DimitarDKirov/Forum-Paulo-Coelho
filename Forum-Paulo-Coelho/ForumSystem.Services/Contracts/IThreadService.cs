namespace ForumSystem.Services.Contracts
{
    using System.Linq;
    using ForumSystem.Models;

    public interface IThreadService
    {
        IQueryable<Thread> All();

        void Add(string title, string content, string creator);
    }
}

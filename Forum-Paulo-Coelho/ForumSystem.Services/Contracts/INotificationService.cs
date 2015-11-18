namespace ForumSystem.Services.Contracts
{
    using System.Linq;

    public interface INotificationService
    {
        IQueryable<string> All(string username);
    }
}

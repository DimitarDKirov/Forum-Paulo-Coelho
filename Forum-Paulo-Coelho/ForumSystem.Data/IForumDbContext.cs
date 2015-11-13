namespace ForumSystem.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using ForumSystem.Models;

    public interface IForumDbContext
    {
        IDbSet<Thread> Threads { get; set; }

        IDbSet<Post> Posts { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<Category> Categories { get; set; }

        IDbSet<Vote> Votes { get; set; }

        IDbSet<Notification> Notifications { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        void Dispose();

        int SaveChanges();
    }
}

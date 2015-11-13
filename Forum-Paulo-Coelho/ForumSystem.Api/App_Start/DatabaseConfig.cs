namespace ForumSystem.Api
{
    using System.Data.Entity;
    using ForumSystem.Data;
    using ForumSystem.Data.Migrations;

    public class DatabaseConfig
    {
        public static void Initialize()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ForumDbContext, Configuration>());
            ForumDbContext.Create().Database.Initialize(true);
        }
    }
}
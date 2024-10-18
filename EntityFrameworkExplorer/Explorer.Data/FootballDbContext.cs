using Castle.Core.Configuration;
using Explorer.Data.Configurations;
using Explorer.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Explorer.Data
{
    public class FootballDbContext : DbContext
    {
        public string DbPath { get; set; }

        // Works for Default Constructor
        // builder.Services.AddDbContext<FootballDbContext>(); Without Options Object
        //public FootballDbContext()
        //{
        //    // Settings specific for SqlLite DB
        //    var folder = Environment.SpecialFolder.LocalApplicationData;
        //    var path = Environment.GetFolderPath(folder);
        //    DbPath = Path.Combine(path, "Football.db");
        //}

        public FootballDbContext(DbContextOptions<FootballDbContext> options):base(options) 
        {
                
        }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<TeamsAndLeaguesView> TeamsAndLeaguesView { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    // Bad Idea to Keep the Connection String Here!! */
        //    optionsBuilder.UseSqlite($"Data Source={DbPath}")
        //        .UseLazyLoadingProxies()
        //        //.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)  // Globally setting as No Tracking 
        //        .LogTo(Console.WriteLine, LogLevel.Information)
        //        .EnableSensitiveDataLogging()
        //        .EnableDetailedErrors();
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // One Way
            //modelBuilder.ApplyConfiguration(new TeamConfiguration());
            //modelBuilder.ApplyConfiguration(new LeagueConfiguration());

            // Second Way
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            // View Configuration
            modelBuilder.Entity<TeamsAndLeaguesView>().HasNoKey().ToView("view_team_league");

            // User Defined Function Configuration
            modelBuilder.HasDbFunction(typeof(FootballDbContext).GetMethod(nameof(GetEarliestTeamMatch), new[] {typeof(int)})).HasName("fn_FuntionNameInDatabase");
        }

        // Pre Configuration Convention Models
        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<string>().HaveMaxLength(100);
            configurationBuilder.Properties<decimal>().HavePrecision(15,2);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<BaseDomainModel>().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
            foreach (var entry in entries)
            {
                entry.Entity.ModificationDate = DateTime.UtcNow;
                entry.Entity.ModifiedBy = "Demo User ONE";

                if(entry.State == EntityState.Added)
                {
                    entry.Entity.ModificationDate = DateTime.UtcNow;
                    entry.Entity.ModifiedBy = "Demo User ONE";
                }

                entry.Entity.Version = Guid.NewGuid();
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        public DateTime GetEarliestTeamMatch(int teamId) => throw new NotImplementedException();    
    }

    public class FootballDbContextFactory : IDesignTimeDbContextFactory<FootballDbContext>
    {
        public FootballDbContext CreateDbContext(string[] args)
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var dbPath = Path.Combine(path,configuration.GetConnectionString("SqlLiteDb"));

            var optionBuilder = new DbContextOptionsBuilder<FootballDbContext>();
            optionBuilder.UseSqlite($"Data Source={dbPath}");

            return new FootballDbContext(optionBuilder.Options);
        }
    }
}

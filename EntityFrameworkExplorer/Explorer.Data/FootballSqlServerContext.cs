using Explorer.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Data
{
    public class FootballSqlServerContext : DbContext
    {
        public FootballSqlServerContext()
        {
                
        }

        public DbSet<Team> Teams { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<League> Leagues { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=Football; Encrypt=true;", 
                sqlServerOptions =>
                {
                    sqlServerOptions.EnableRetryOnFailure(maxRetryCount:5, maxRetryDelay:TimeSpan.FromSeconds(5), errorNumbersToAdd:null);
                });
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

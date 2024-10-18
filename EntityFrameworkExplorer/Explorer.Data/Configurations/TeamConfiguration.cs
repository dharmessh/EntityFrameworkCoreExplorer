using Explorer.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Data.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasIndex(q => q.Name).IsUnique();

            // Row Version Property - For SQL Server - Alternate Way
            //builder.Property(q => q.Version).IsRowVersion();

            // Row Version Property - For Other DB - Alternate Way
            //builder.Property(q => q.Version).IsConcurrencyToken();

            // Making Table Temporal
            builder.ToTable("Teams",b => b.IsTemporal());

            builder.HasMany(a => a.HomeMatches).WithOne(a => a.HomeTeam).HasForeignKey(a => a.HomeTeamId).IsRequired().OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(a => a.AwayMatches).WithOne(a => a.AwayTeam).HasForeignKey(a => a.AwayTeamId).IsRequired().OnDelete(DeleteBehavior.Restrict);

            builder.HasData(
               new Team
               {
                   Id = 1,
                   Name = "India",
                   CreationDate = DateTime.Now,
                   LeagueId = 1,
                   CoachId = 1
               },
                new Team
                {
                    Id = 2,
                    Name = "Bangladesh",
                    CreationDate = DateTime.Now,
                    LeagueId = 1,
                    CoachId = 2
                },
                 new Team
                 {
                     Id = 3,
                     Name = "Russia",
                     CreationDate = DateTime.Now,
                     LeagueId = 1,
                     CoachId = 3
                 }
               );

        }
    }
}

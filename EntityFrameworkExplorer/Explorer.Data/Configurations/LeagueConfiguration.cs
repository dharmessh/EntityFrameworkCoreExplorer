using Explorer.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Explorer.Data.Configurations
{
    public class LeagueConfiguration : IEntityTypeConfiguration<League>
    {
        public void Configure(EntityTypeBuilder<League> builder)
        {
            // Global Query Filter will be applied automatically
            // Note : Multiple Query Filter Doesnt Works, It will be apply only last one of all.
            builder.HasQueryFilter(q => q.IsDeleted == false);

            builder.HasData(
                new League
                {
                    Id = 1,
                    Name = "India US League",
                },
                 new League
                 {
                     Id = 2,
                     Name = "Bangladesh UAE League",
                 },
                  new League
                  {
                      Id = 3,
                      Name = "Russia Greenland League",
                  }
                );
        }
    }
}

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
    public class CoachConfiguration :IEntityTypeConfiguration<Coach>
    {
        public void Configure(EntityTypeBuilder<Coach> builder)
        {
            builder.HasData(
                new Coach
                {
                    Id = 1,
                    Name = "Sachin",
                },
                 new Coach
                 {
                     Id = 2,
                     Name = "Rohit Sharma",
                 },
                  new Coach
                  {
                      Id = 3,
                      Name = "Rahul Dravid",
                  }
                );
        }
    }
}

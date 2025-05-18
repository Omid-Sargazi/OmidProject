using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using OmidProject.Domains.Domain.General;

namespace OmidProject.Infrastructures.CommandDb.EntitiesConfiguration.General
{
    public class DistrictConfiguration : IEntityTypeConfiguration<District>
    {
        public void Configure(EntityTypeBuilder<District> builder)
        {
            builder.HasOne(x => x.City)
                .WithMany(x => x.Districts)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}

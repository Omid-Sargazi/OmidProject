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
    public class AdvertisementImageConfiguration : IEntityTypeConfiguration<AdvertisementImage>
    {
        public void Configure(EntityTypeBuilder<AdvertisementImage> builder)
        {
            builder.HasOne(x => x.Advertisement)
                .WithMany(x => x.AdvertisementImages)
                .HasForeignKey(x =>x.AdvertisementId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

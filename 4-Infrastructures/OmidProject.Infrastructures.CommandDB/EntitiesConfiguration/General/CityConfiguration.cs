using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmidProject.Domains.Domain.General;

namespace OmidProject.Infrastructures.CommandDb.EntitiesConfiguration.General;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasOne(x => x.Province)
            .WithMany(x => x.Cities)
            .HasForeignKey(x => x.ProvinceId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}




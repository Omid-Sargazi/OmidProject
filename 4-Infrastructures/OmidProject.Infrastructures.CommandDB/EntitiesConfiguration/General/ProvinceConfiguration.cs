using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OmidProject.Domains.Domain.General;

namespace OmidProject.Infrastructures.CommandDb.EntitiesConfiguration.General;

public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
    }
}
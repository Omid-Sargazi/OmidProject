using OmidProject.Domains.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OmidProject.Infrastructures.CommandDb.EntitiesConfiguration.General;

public class AccessibleFormConfiguration : IEntityTypeConfiguration<AccessibleForm>
{
    public void Configure(EntityTypeBuilder<AccessibleForm> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
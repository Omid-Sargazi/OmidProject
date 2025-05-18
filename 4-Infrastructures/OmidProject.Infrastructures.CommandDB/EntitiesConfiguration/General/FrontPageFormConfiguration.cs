using OmidProject.Domains.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OmidProject.Infrastructures.CommandDb.EntitiesConfiguration.General;

public class FrontPageFormConfiguration : IEntityTypeConfiguration<FrontPageForm>
{
    public void Configure(EntityTypeBuilder<FrontPageForm> builder)
    {
        builder.HasKey(x => x.Id);
    }
}
using OmidProject.Domains.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OmidProject.Infrastructures.CommandDb.EntitiesConfiguration.General;

public class RoleFrontPageFormConfiguration : IEntityTypeConfiguration<RoleFrontPageForm>
{
    public void Configure(EntityTypeBuilder<RoleFrontPageForm> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.ApplicantRole)
            .WithMany(x => x.RoleFrontPageForms)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.FrontPageForm)
            .WithMany(x => x.RoleFrontPageForm)
            .HasForeignKey(x => x.FrontPageFormId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
using OmidProject.Domains.Domain.General;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OmidProject.Infrastructures.CommandDb.EntitiesConfiguration.General;

public class RoleAccessibleFormConfiguration : IEntityTypeConfiguration<RoleAccessibleForm>
{
    public void Configure(EntityTypeBuilder<RoleAccessibleForm> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.ApplicantRole)
            .WithMany(x => x.RoleAccessibleForms)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(x => x.AccessibleForm)
            .WithMany(x => x.RoleAccessibleForm)
            .HasForeignKey(x => x.AccessibleFormId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
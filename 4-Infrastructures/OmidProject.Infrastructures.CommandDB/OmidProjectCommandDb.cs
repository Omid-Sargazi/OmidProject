using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace OmidProject.Infrastructures.CommandDb;

public sealed class OmidProjectCommandDb : OmidProjectBaseCommandDb
{
    public OmidProjectCommandDb(DbContextOptions<OmidProjectBaseCommandDb> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var isDeletedProperty = entityType.FindProperty("IsDeleted");
            if (isDeletedProperty != null && isDeletedProperty.ClrType == typeof(bool))
            {
                var parameter = Expression.Parameter(entityType.ClrType, "p");
                var filter = Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, isDeletedProperty.PropertyInfo),
                        Expression.Constant(false, typeof(bool))
                    )
                    , parameter);
                entityType.SetQueryFilter(filter);
            }
        }

        base.OnModelCreating(modelBuilder);
    }
}
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace OmidProject.Applications.Contracts.CustomAttribute;

[AttributeUsage(AttributeTargets.Property)]
public class ExistsInDatabaseAttribute : ValidationAttribute
{
    private readonly string _entityName;
    private readonly string _idPropertyName;

    public ExistsInDatabaseAttribute(string entityName, string idPropertyName)
    {
        _entityName = entityName;
        _idPropertyName = idPropertyName;
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var serviceProvider = validationContext.GetService<IServiceProvider>();
        var dbContext = serviceProvider.GetService(typeof(IdentityDbContext)) as DbContext;

        if (dbContext == null)
            throw new InvalidOperationException("DbContext is required to validate ExistsInDatabase attribute.");

        var entityType = dbContext.Model.FindEntityType(_entityName);
        if (entityType == null) return new ValidationResult($"Entity {_entityName} not found.");

        var idProperty = entityType.FindProperty(_idPropertyName);
        if (idProperty == null)
            return new ValidationResult($"Property {_idPropertyName} not found in entity {_entityName}.");

        var dbSet = typeof(DbContext).GetMethod("Set", Type.EmptyTypes)
            .MakeGenericMethod(entityType.ClrType)
            .Invoke(dbContext, null);

        var queryable = dbSet as IQueryable<object>;

        var parameter = Expression.Parameter(entityType.ClrType, "e");
        var propertyAccess = Expression.Property(parameter, idProperty.PropertyInfo.Name);
        var constantValue = Expression.Constant(value);
        var equality = Expression.Equal(propertyAccess, constantValue);
        var lambda = Expression.Lambda(equality, parameter);

        var existsMethod = typeof(Queryable).GetMethods()
            .First(m => m.Name == "Any" && m.GetParameters().Length == 2)
            .MakeGenericMethod(entityType.ClrType);

        var exists = (bool)existsMethod.Invoke(null, new object[] { queryable, lambda });

        if (!exists) return new ValidationResult($"The value '{value}' does not exist in {_entityName}.");

        return ValidationResult.Success;
    }
}
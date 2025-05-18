using OmidProject.Frameworks.Contracts.Abstracts.EntityAbstract;

namespace OmidProject.Domains.Domain.General;

public class AccessibleForm : Entity<int> , IEqualityComparer<AccessibleForm>
{
    public AccessibleForm()
    {
    }

    public AccessibleForm(string title, string route)
    {
        Title = title;
        Route = route;
    }

    public string Title { get; protected set; }
    public string Route { get; protected set; }
    public List<RoleAccessibleForm> RoleAccessibleForm { get; set; }


    public void Update(string title, string route)
    {
        Title = title;
        Route = route;
    }

    public bool Equals(AccessibleForm x, AccessibleForm y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null)) return false;
        if (ReferenceEquals(y, null)) return false;
        if (x.GetType() != y.GetType()) return false;
        return x.Id == y.Id && x.IsDeleted == y.IsDeleted && x.CreatedBy.Equals(y.CreatedBy) && x.CreatedAt.Equals(y.CreatedAt) && Nullable.Equals(x.ModifiedBy, y.ModifiedBy) && Nullable.Equals(x.ModifiedAt, y.ModifiedAt) && x.Title == y.Title && x.Route == y.Route && x.RoleAccessibleForm.Equals(y.RoleAccessibleForm);
    }

    public int GetHashCode(AccessibleForm obj)
    {
        var hashCode = new HashCode();
        hashCode.Add(obj.Id);
        hashCode.Add(obj.IsDeleted);
        hashCode.Add(obj.CreatedBy);
        hashCode.Add(obj.CreatedAt);
        hashCode.Add(obj.ModifiedBy);
        hashCode.Add(obj.ModifiedAt);
        hashCode.Add(obj.Title);
        hashCode.Add(obj.Route);
        hashCode.Add(obj.RoleAccessibleForm);
        return hashCode.ToHashCode();
    }
}
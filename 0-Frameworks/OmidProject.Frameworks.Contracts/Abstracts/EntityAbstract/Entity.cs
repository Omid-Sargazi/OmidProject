namespace OmidProject.Frameworks.Contracts.Abstracts.EntityAbstract;

public abstract class Entity<T> : ICloneable
{
    public T Id { get; protected set; }
    public bool IsDeleted { get; protected set; }
    public Guid CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? ModifiedBy { get; set; }
    public DateTime? ModifiedAt { get; set; }

    public object Clone()
    {
        return MemberwiseClone();
    }

    public void SetDelete()
    {
        IsDeleted = true;
    }

    public bool Equals<T>(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        return obj.GetType() == GetType() && Equals((T)obj);
    }
}
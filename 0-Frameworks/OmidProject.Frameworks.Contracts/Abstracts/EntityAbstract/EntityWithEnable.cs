namespace OmidProject.Frameworks.Contracts.Abstracts.EntityAbstract;

public abstract class EntityWithEnable<T> : Entity<T>
{
    public bool IsEnabled { get; protected set; }

    public void SetEnable()
    {
        IsEnabled = true;
    }

    public void SetDisable()
    {
        IsEnabled = false;
    }
}
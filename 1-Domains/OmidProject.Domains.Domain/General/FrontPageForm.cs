using OmidProject.Frameworks.Contracts.Abstracts.EntityAbstract;

namespace OmidProject.Domains.Domain.General;

public class FrontPageForm : Entity<int>
{
    public FrontPageForm()
    {
    }

    public FrontPageForm(string title, string route)
    {
        Title = title;
        Route = route;
    }

    public string Title { get; protected set; }
    public string Route { get; protected set; }
    public List<RoleFrontPageForm> RoleFrontPageForm { get; set; }


    public void Update(string title, string route)
    {
        Title = title;
        Route = route;
    }
}
using OmidProject.Applications.Contracts.Service;

namespace OmidProject.Infrastructures.Configurations.DefaultData;

public class InitialDataLoader(IRoleAccessibleFormService roleAccessibleFormService)
{
    public async Task Execute()
    {
        await LoadRoleAccessibleFormCache();
    }

    private async Task LoadRoleAccessibleFormCache()
    {
        await roleAccessibleFormService.ReadAllFromCache();
    }
}
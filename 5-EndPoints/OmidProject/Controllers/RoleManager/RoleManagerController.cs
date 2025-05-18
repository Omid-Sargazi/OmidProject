using System.Threading;
using System.Threading.Tasks;
using OmidProject.Applications.Contracts.RoleManagerContracts.Commands;
using OmidProject.Applications.Contracts.RoleManagerContracts.Queries;
using OmidProject.Frameworks.Contracts.Markers;
using Microsoft.AspNetCore.Mvc;

namespace OmidProject.Host.Controllers.RoleManager;

public class RoleManagerController : MainController
{
    public RoleManagerController(IDistributor distributor) : base(distributor)
    {
    }

    [HttpPost("create-role")]
    public async Task<IActionResult> CreateRole(CreateRoleCommand query, CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PushCommand<CreateRoleCommand, CreateRollCommandResponse>(query, cancellationToken);

        return OkApiResult(result);
    }

    [HttpDelete("delete-role")]
    public async Task<IActionResult> DeleteRole(DeleteRoleCommand query, CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PushCommand<DeleteRoleCommand, DeleteRoleCommandResponse>(query, cancellationToken);

        return OkApiResult(result);
    }

    [HttpPut("update-role")]
    public async Task<IActionResult> UpdateRole(UpdateRoleCommand query, CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PushCommand<UpdateRoleCommand, UpdateRoleCommandResponse>(query, cancellationToken);

        return OkApiResult(result);
    }

    [HttpGet("read-role")]
    public async Task<IActionResult> ReadRole(CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PullQuery<ReadRoleQuery, ReadRoleQueryResponse>(new ReadRoleQuery(), cancellationToken);

        return OkApiResult(result);
    }
}
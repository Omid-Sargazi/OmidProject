using System.Threading;
using System.Threading.Tasks;
using OmidProject.Applications.Contracts.SystemMessageContracts.Queries;
using OmidProject.Applications.Contracts.UserManagerContracts.Commands;
using OmidProject.Applications.Contracts.UserManagerContracts.Queries;
using OmidProject.Frameworks.Contracts.Markers;
using Microsoft.AspNetCore.Mvc;

namespace OmidProject.Host.Controllers.UserManager;

public class UserManagerController : MainController
{
    public UserManagerController(IDistributor distributor) : base(distributor)
    {
    }

    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser(CreateUserCommand query, CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PushCommand<CreateUserCommand, CreateUserCommandResponse>(query, cancellationToken);

        return OkApiResult(result);
    }

    [HttpPost("read-user")]
    public async Task<IActionResult> ReadUser(ReadUserQuery query, CancellationToken cancellationToken)
    {
        var result = await Distributor.PullQuery<ReadUserQuery, ReadUserQueryResponse>(query, cancellationToken);

        return OkApiResult(result);
    }

    [HttpDelete("delete-user")]
    public async Task<IActionResult> DeleteUser(DeleteUserCommand query, CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PushCommand<DeleteUserCommand, DeleteUserCommandResponse>(query, cancellationToken);

        return OkApiResult(result);
    }

    [HttpPut("update-user")]
    public async Task<IActionResult> ReadUser(UpdateUserCommand command, CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PushCommand<UpdateUserCommand, UpdateUserCommandResponse>(command, cancellationToken);

        return OkApiResult(result);
    }

    [HttpPost("add-role-to-user")]
    public async Task<IActionResult> AddRoleToUser(AddRoleToUserCommand command, CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PushCommand<AddRoleToUserCommand, AddRoleToUserCommandResponse>(command,
                cancellationToken);

        return OkApiResult(result);
    }

    [HttpPost("read-user-role")]
    public async Task<IActionResult> ReadUserRole(ReadUserRoleQuery command, CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PullQuery<ReadUserRoleQuery, ReadUserRoleQueryResponse>(command, cancellationToken);

        return OkApiResult(result);
    }

    [HttpDelete("delete-user-role")]
    public async Task<IActionResult> DeleteUserRole(DeleteUserRoleCommand command, CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PushCommand<DeleteUserRoleCommand, DeleteUserRoleCommandResponse>(command,
                cancellationToken);

        return OkApiResult(result);
    }
}
using System.Threading;
using System.Threading.Tasks;
using OmidProject.Applications.Contracts.SystemMessageContracts.Commands;
using OmidProject.Applications.Contracts.SystemMessageContracts.Queries;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Contracts.Markers;
using Microsoft.AspNetCore.Mvc;

namespace OmidProject.Host.Controllers.SystemMessage;

public class SystemErrorController : MainController
{
    public SystemErrorController(IDistributor distributor) : base(distributor)
    {
    }

    [HttpGet("get-all-system-errors")]
    public async Task<IActionResult> GetAllSystemErrors(CancellationToken cancellation)
    {
        var query = new GetAllSystemErrorsQuery();

        var result =
            await Distributor.PullQuery<GetAllSystemErrorsQuery, GetAllSystemErrorsQueryResponse>(query, cancellation);

        return OkApiResult(result.List);
    }

    [HttpPost("create-system-error-command")]
    public async Task<IActionResult> CreateSystemError(CreateSystemMessageCommand command,
        CancellationToken cancellation)
    {
        var result = await Distributor.PushCommand<CreateSystemMessageCommand, CommandResponse>(command, cancellation);

        return OkApiResult(result);
    }

    [HttpPut("update-system-error-command")]
    public async Task<IActionResult> UpdateSystemError(UpdateSystemMessageCommand command,
        CancellationToken cancellation)
    {
        var result = await Distributor.PushCommand<UpdateSystemMessageCommand, CommandResponse>(command, cancellation);

        return OkApiResult(result);
    }

    [HttpDelete("delete-system-error-message-by-language")]
    public async Task<IActionResult> DeleteSystemErrorMessageByLanguage(DeleteSystemMessageByLanguageCommand command,
        CancellationToken cancellation)
    {
        var result =
            await Distributor.PushCommand<DeleteSystemMessageByLanguageCommand, CommandResponse>(command, cancellation);

        return OkApiResult(result);
    }
}
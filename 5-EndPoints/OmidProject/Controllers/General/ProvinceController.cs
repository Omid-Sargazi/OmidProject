using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmidProject.Applications.Contracts.ProvinceContracts.Commands;
using OmidProject.Applications.Contracts.ProvinceContracts.Queries;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Host.Controllers.General;

public class ProvinceController : MainController
{
    public ProvinceController(IDistributor distributor) : base(distributor)
    {
    }

    
    [HttpPost("create-province")]
    public async Task<IActionResult> CreateProvince(CreateProvinceCommand command,
        CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PushCommand<CreateProvinceCommand, CreateProvinceCommandResponse>(
                command, cancellationToken);

        return OkApiResult(result);
    }

    [HttpPost("update-province")]
    public async Task<IActionResult> UpdateProvince(UpdateProvinceCommand command, CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PushCommand<UpdateProvinceCommand, UpdateProvinceCommandResponse>(command,
                cancellationToken);

        return OkApiResult(result);
    }

    [HttpPost("delete-province")]
    public async Task<IActionResult> DeleteProvince(DeleteProvinceCommand command, CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PushCommand<DeleteProvinceCommand, DeleteProvinceCommandResponse>(command,
                cancellationToken);
        return OkApiResult(result);
    }

    [HttpGet("get-province")]
    public async Task<IActionResult> GetProvince([FromQuery] GetProvinceQuery query,
        CancellationToken cancellationToken)
    {
        var result = await Distributor.PullQuery<GetProvinceQuery, GetProvinceQueryResponse>(query, cancellationToken);
        return OkApiResult(result);
    }
}
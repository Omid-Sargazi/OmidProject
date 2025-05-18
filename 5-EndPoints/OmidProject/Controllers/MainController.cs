using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Contracts.Common;
using OmidProject.Frameworks.Contracts.Markers;
using OmidProject.Host.CustomAttribute;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OmidProject.Host.Controllers;

#if DEBUG
[AllowAnonymous]
#endif
[CustomAuthorize]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
//[SystemMessageActionFilter]
public class MainController : ControllerBase
{
    protected readonly IDistributor Distributor;

    public MainController(IDistributor distributor)
    {
        Distributor = distributor;
    }


    protected IActionResult OkApiResult(dynamic tResult)
    {
        return Ok(new ApiResult(CommandResponseSuccessful.CreateSuccessful(), tResult));
    }

    protected IActionResult OkApiResult()
    {
        return Ok(new ApiResult(CommandResponseSuccessful.CreateSuccessful()));
    }

    protected IActionResult OkApiCommandResponseResult(CommandResponse tResult)
    {
        return Ok(new ApiResult(tResult));
    }

    protected IActionResult OkApiFileResult(byte[] bytes, string contentType)
    {
        return File(bytes, contentType);
    }
}
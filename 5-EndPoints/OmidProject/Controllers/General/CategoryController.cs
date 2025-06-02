using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OmidProject.Applications.Contracts.CategoryContracts.Queries;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Host.Controllers.General;

[AllowAnonymous]
public class CategoryController:MainController
{
    public CategoryController(IDistributor distributor) : base(distributor)
    {
    }

    [HttpGet("get-all-categories")]
    public async Task<IActionResult> GetAllCategories([FromQuery] GetAllCategoriesQuery query,
        CancellationToken cancellationToken)
    {
        var result =
            await Distributor.PullQuery<GetAllCategoriesQuery, GetAllCategoriesQueryResponse>(query,
                cancellationToken);
        return OkApiResult(result);
    }
}
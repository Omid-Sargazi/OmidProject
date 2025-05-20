using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OmidProject.Applications.Contracts.CityContracts.Commands;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Host.Controllers.General
{
    public class CityController : MainController
    {
        public CityController(IDistributor distributor) : base(distributor)
        {
        }

        [HttpPost("create-city")]
        public async Task<IActionResult> CreateCity(CreateCityCommand command,CancellationToken cancellationToken)
        {
            var result =
                await Distributor.PushCommand<CreateCityCommand, CreateCityCommandReposne>(command, cancellationToken);
            return OkApiResult(result);
        }
    }
}

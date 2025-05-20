using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OmidProject.Applications.Application.AdvertismentImageContracts.Commands;
using OmidProject.Applications.Application.AdvertismentImageContracts.Queries;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Host.Controllers.General
{
    public class AdvertisementImageController : MainController
    {
        public AdvertisementImageController(IDistributor distributor) : base(distributor)
        {
        }

        [HttpPost("create-advertisementImage")]
        public async Task<IActionResult> CreateAdvertisementImage(CreateAdvertisementImageCommand command,
            CancellationToken cancellationToken)
        {
            var result = 
                await Distributor.PushCommand<CreateAdvertisementImageCommand, CreateAdvertisementImageCommandResponse>(command, cancellationToken);
            return OkApiResult(result);
        }

        [HttpGet("get-advertisementImage")]
        public async Task<IActionResult> GetAdvertisementImage([FromQuery] GetAllImagesOfAdvertisementQuery query,
            CancellationToken cancellationToken)
        {
            var result =
                await Distributor.PullQuery<GetAllImagesOfAdvertisementQuery, GetAllAdvertisementImageQueryResponse>(query,
                    cancellationToken);
            return OkApiResult(result);
        }
    }
}


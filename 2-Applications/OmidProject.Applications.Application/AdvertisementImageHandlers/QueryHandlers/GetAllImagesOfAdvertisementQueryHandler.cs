using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmidProject.Applications.Application.AdvertismentImageContracts.Queries;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Application.AdvertisementImageHandlers.QueryHandlers
{
    // public class GetAllImagesOfAdvertisementQueryHandler : IQueryHandler<GetAllImagesOfAdvertisementQuery,GetAllImagesOfAdvertisementQueryResponse>
    // {
    //     private IAdvertisementImageRepository _advertisementImageRepository;
    //     public GetAllImagesOfAdvertisementQueryHandler(IAdvertisementImageRepository advertisementImageRepository)
    //     {
    //         _advertisementImageRepository = advertisementImageRepository;
    //     }
    //
    //     // public async Task<GetAllImagesOfAdvertisementQueryResponse> Execute(GetAllImagesOfAdvertisementQuery query, CancellationToken cancellationToken)
    //     // {
    //     //     // var imageOfAdvertisement = await _advertisementImageRepository.GetByIdAsync(query.AdvertisementId);
    //     //
    //     // }
    //     public Task<GetAllImagesOfAdvertisementQueryResponse> Execute(GetAllImagesOfAdvertisementQuery query, CancellationToken cancellationToken)
    //     {
    //         throw new NotImplementedException();
    //     }
    // }
}

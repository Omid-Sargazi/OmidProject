using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmidProject.Applications.Application.AdvertismentImageContracts.Queries.DTOs;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Abstracts.QueryAbstract;

namespace OmidProject.Applications.Application.AdvertismentImageContracts.Queries
{
    public class GetAllImagesOfAdvertisementQuery : Query
    {
        public int AdvertisementId { get; set; }
    }

    public class GetAllImagesOfAdvertisementQueryResponse : QueryResponse
    {
        public List<AdvertisementImageDto> AdvertisementImage { get; set;}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmidProject.Applications.Contracts.AdvertisementContracts.Queries.DTOs;
using OmidProject.Frameworks.Contracts.Abstracts.QueryAbstract;

namespace OmidProject.Applications.Contracts.AdvertisementContracts.Queries
{
    public class GetAdvertisementQuery : Query
    {
        public int Id { get; set; }
    }


    public class GetAdvertisementQueryResponse : QueryResponse
    {
        public AdvertisementDto Item { get; set; }
    }
}

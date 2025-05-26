using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.AdvertisementContracts.Commands
{
    public class UpdateAdvertisementCommand : Command
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public decimal Price { get; set; }
        public int DistrictId { get; set; }
    }

    public class UpdateAdvertisementCommandResponse : Command
    {

    }
}

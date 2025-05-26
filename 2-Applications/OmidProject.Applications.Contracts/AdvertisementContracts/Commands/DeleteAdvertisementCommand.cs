using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Contracts.AdvertisementContracts.Commands
{
    public class DeleteAdvertisementCommand : Command
    {
        public int Id { get; set; }
    }

    public class DeleteAdvertisementCommandResponse : CommandResponse
    {

    }
}

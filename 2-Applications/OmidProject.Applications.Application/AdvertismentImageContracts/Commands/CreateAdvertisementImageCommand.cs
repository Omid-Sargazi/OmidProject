using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Application.AdvertismentImageContracts.Commands
{
    public class CreateAdvertisementImageCommand : Command
    {
        public Advertisement advertisement { get; set; }
    }

    public class CreateAdvertisementImageCommandResponse : CommandResponse
    {
        public int Id { get; set; }
    }

}

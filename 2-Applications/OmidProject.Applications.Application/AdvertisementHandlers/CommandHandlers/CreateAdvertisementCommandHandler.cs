using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmidProject.Applications.Contracts.AdvertisementContracts.Commands;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;

namespace OmidProject.Applications.Application.AdvertisementHandlers.CommandHandlers
{
    public class CreateAdvertisementCommandHandler : CommandHandler<CreateAdvertisementCommand, CreateAdvertisementCommandResponse>
    {
        
        public override Task<CreateAdvertisementCommandResponse> Executor(CreateAdvertisementCommand command, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmidProject.Applications.Contracts.CityContracts.Commands;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Frameworks.Contracts.Markers;

namespace OmidProject.Applications.Application.CityHandlers.CommandHandlers
{
    public class DeleteCityCommandHandler : ICommandHandler<DeleteCityCommand,DeleteCityCommandResponse>
    {
        private readonly ICityRepository _cityRepository;
        public DeleteCityCommandHandler(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public Task<DeleteCityCommandResponse> Execute(DeleteCityCommand command, CancellationToken cancellationToken)
        {
            Guard(command);
            throw new NotImplementedException();
        }

        public async Task Guard(DeleteCityCommand command)
        {

        }
    }
}

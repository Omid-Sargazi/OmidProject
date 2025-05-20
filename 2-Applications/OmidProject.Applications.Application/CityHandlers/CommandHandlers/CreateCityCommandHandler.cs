using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmidProject.Applications.Application.ProvinceHandlers.CommandHandlers.Exceptions;
using OmidProject.Applications.Contracts.CityContracts.Commands;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Domains.Domain.General;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Applications.Application.CityHandlers.CommandHandlers
{
    public class CreateCityCommandHandler : CommandHandler<CreateCityCommand, CreateCityCommandReposne>
    {
        private readonly ICityRepository _cityRepository;
        public CreateCityCommandHandler(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public override async Task<CreateCityCommandReposne> Executor(CreateCityCommand command, CancellationToken cancellationToken)
        {
            var city = new City(command.Name, command.ProvinceId);

            Guard(command);
            await _cityRepository.AddAsync(city);
            var result = new CreateCityCommandReposne();
            result.Id = city.Id;
            return result;
        }

        private async Task Guard(CreateCityCommand command)
        {
            if (command.ProvinceId >= 33)
                throw new ProvinceIdIsGreaterThan();
            if (command.Name.IsNullOrEmpty())
                throw new ProvinceNameIsNullOrEmptyException();
            
        }
    }
}

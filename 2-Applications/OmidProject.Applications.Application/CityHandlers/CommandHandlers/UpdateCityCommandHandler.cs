using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmidProject.Applications.Application.AccessibleFormHandlers.Exception;
using OmidProject.Applications.Contracts.CityContracts.Commands;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Applications.Application.CityHandlers.CommandHandlers
{
    public class UpdateCityCommandHandler: CommandHandler<UpdateCityCommand, UpdateCityCommandResponse>
    {
        private readonly ICityRepository _cityRepository;

        public UpdateCityCommandHandler(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }
        public override async Task<UpdateCityCommandResponse> Executor(UpdateCityCommand command, CancellationToken cancellationToken)
        {
            await Guard(command);
            var city = await _cityRepository.GetByIdAsync(command.Id);
            if (city == null)
                throw new ArgumentException("City Not Found.");
            city.Update(city.Name, command.ProvinceId);
            await _cityRepository.UpdateAsync(city);
            var result = new UpdateCityCommandResponse();
            return result;
        }

        private async Task Guard(UpdateCityCommand command)
        {
            if (command.Id.IsInvalidId())
                throw new IdIsInvalidIdException();
            if (command.Name.IsNullOrEmpty())
                throw new ArgumentException();
        }
    }
}

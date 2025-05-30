﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmidProject.Applications.Application.AccessibleFormHandlers.Exception;
using OmidProject.Applications.Contracts.CityContracts.Commands;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Frameworks.Contracts.Markers;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Applications.Application.CityHandlers.CommandHandlers
{
    public class DeleteCityCommandHandler : ICommandHandler<DeleteCityCommand,DeleteCityCommandResponse>
    {
        private readonly ICityRepository _cityRepository;
        public DeleteCityCommandHandler(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        public async Task<DeleteCityCommandResponse> Execute(DeleteCityCommand command, CancellationToken cancellationToken)
        {
            Guard(command);
            var city = await _cityRepository.GetByIdAsync(command.Id);
            if (city == null)
                throw new AggregateException();
            await _cityRepository.DeleteAsync(city);
            var result = new DeleteCityCommandResponse();
            return result;
        }

        public async Task Guard(DeleteCityCommand command)
        {
            if (command.Id.IsInvalidId())
                throw new IdIsInvalidIdException();
        }
    }
}

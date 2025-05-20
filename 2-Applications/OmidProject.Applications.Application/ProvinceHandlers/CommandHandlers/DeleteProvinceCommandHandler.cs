using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmidProject.Applications.Application.AccessibleFormHandlers.Exception;
using OmidProject.Applications.Application.ProvinceHandlers.CommandHandlers.Exceptions;
using OmidProject.Applications.Contracts.ProvinceContracts.Commands;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Frameworks.Contracts.Abstracts.CommandAbstract;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Applications.Application.ProvinceHandlers.CommandHandlers
{
    public class DeleteProvinceCommandHandler : CommandHandler<DeleteProvinceCommand,DeleteProvinceCommandResponse>
    {
        private readonly IProvinceRepository _ProvinceRepository;

        public DeleteProvinceCommandHandler(IProvinceRepository provinceRepository)
        {
            _ProvinceRepository = provinceRepository;
        }
        public override async Task<DeleteProvinceCommandResponse> Executor(DeleteProvinceCommand command, CancellationToken cancellationToken)
        {
            Guard(command);
            var province = await _ProvinceRepository.GetByIdAsync(command.Id);
            if (province == null) 
                throw new ProvinceNotExistException();
            await _ProvinceRepository.DeleteAsync(province);
            var result = new DeleteProvinceCommandResponse();
            return result;
        }

        private async Task Guard(DeleteProvinceCommand command)
        {
            if (command.Id.IsInvalidId())
                throw new IdIsInvalidIdException();
        }
    }
}

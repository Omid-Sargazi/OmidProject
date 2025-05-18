using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmidProject.Applications.Application.AccessibleFormHandlers.Exception;
using OmidProject.Applications.Contracts.ProvinceContracts.Queries;
using OmidProject.Applications.Contracts.ProvinceContracts.Queries.DTOs;
using OmidProject.Applications.Contracts.Repository.General;
using OmidProject.Applications.Contracts.Service;
using OmidProject.Frameworks.Contracts.Markers;
using OmidProject.Frameworks.Utilities.Extensions;

namespace OmidProject.Applications.Application.ProvinceHandlers.QueryHandlers
{
    public class GetProvinceQueryHandler : IQueryHandler<GetProvinceQuery, GetProvinceQueryResponse>
    {
        private readonly IProvinceRepository _ProvinceRepository;
        private readonly IProvinceService _provinceService;

        public GetProvinceQueryHandler(IProvinceRepository provinceRepository, IProvinceService provinceService)
        {
            _ProvinceRepository = provinceRepository;
            _provinceService = provinceService;
        }

        public async Task<GetProvinceQueryResponse> Execute(GetProvinceQuery query, CancellationToken cancellationToken)
        {
            Guard(query);

            var province = await _ProvinceRepository.GetByIdAsync(query.Id);

            var result = new GetProvinceQueryResponse();
            result.Item = _provinceService.ConvertTo(province);
            return result;
        }

        private void Guard(GetProvinceQuery query)
        {
            if (query.Id.IsInvalidId())
                throw new IdIsInvalidIdException();
        }
    }
}

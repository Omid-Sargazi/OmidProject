using OmidProject.Applications.Contracts.RoleManagerContracts.Queries;
using OmidProject.Applications.Contracts.RoleManagerContracts.Queries.DTOs;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Contracts.Markers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OmidProject.Applications.Application.RoleManagerHandlers.QueryHandlers;

public class
    ReadRoleQueryHandler : IQueryHandler<ReadRoleQuery, ReadRoleQueryResponse>
{
    private readonly RoleManager<ApplicationRole> _roleManager;

    public ReadRoleQueryHandler(RoleManager<ApplicationRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<ReadRoleQueryResponse> Execute(ReadRoleQuery query,
        CancellationToken cancellationToken)
    {
        var role = await _roleManager.Roles.ToListAsync(cancellationToken);

        var result = new ReadRoleQueryResponse();

        foreach (var item in role)
        {
            var dto = new RoleDto
            {
                Id = item.Id,
                Name = item.Name,
                NormalizedName = item.Name,
                ConcurrencyStamp = Guid.Parse(item.ConcurrencyStamp)
            };

            result.List.Add(dto);
        }

        return result;
    }
}
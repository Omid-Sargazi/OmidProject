using OmidProject.Applications.Application.Exceptions;
using OmidProject.Applications.Contracts.SystemMessageContracts.Queries;
using OmidProject.Applications.Contracts.SystemMessageContracts.Queries.DTOs;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Contracts.Markers;
using OmidProject.Frameworks.Utilities.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OmidProject.Applications.Application.SystemMessageHandlers.QueryHandlers;

public class ReadUserRoleQueryHandler : IQueryHandler<ReadUserRoleQuery, ReadUserRoleQueryResponse>
{
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public ReadUserRoleQueryHandler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<ReadUserRoleQueryResponse> Execute(ReadUserRoleQuery query, CancellationToken cancellationToken)
    {
        if (query.ObjectIsAnyNullOrEmpty())
        {
            var role = await _roleManager.Roles.ToListAsync(cancellationToken);

            var result = new ReadUserRoleQueryResponse();

            foreach (var roleItem in role)
            {
                var users = await _userManager.GetUsersInRoleAsync(roleItem.Name);
                if (users == null)
                    continue;

                foreach (var usersItem in users)
                {
                    var dto = new AspNetUserRolesDto();
                    dto.RoleName = roleItem.Name;
                    dto.UserName = usersItem.UserName;
                    result.Items.Add(dto);
                }
            }

            return result;
        }

        if (!query.RoleName.IsNullOrEmpty())
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Name == query.RoleName, cancellationToken);
            if (role == null)
                throw new IdentityException("رول مورد نظر وجود ندارد !", "Role is not exist");

            var result = new ReadUserRoleQueryResponse();
            var users = await _userManager.GetUsersInRoleAsync(role.Name);

            foreach (var usersItem in users)
            {
                if (users == null)
                    continue;

                var dto = new AspNetUserRolesDto();
                dto.RoleName = role.Name;
                dto.UserName = usersItem.UserName;
                result.Items.Add(dto);
            }

            return result;
        }
        else
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == query.UserName,
                cancellationToken);

            if (user == null)
                throw new IdentityException("کاربر مورد نظر وجود ندارد !", "User is not exist");

            var result = new ReadUserRoleQueryResponse();
            var roles = await _roleManager.Roles.ToListAsync(cancellationToken);

            foreach (var roleItem in roles)
            {
                var users = await _userManager.GetUsersInRoleAsync(roleItem.Name);
                if (users == null)
                    continue;

                foreach (var usersItem in users)
                    if (usersItem.UserName == query.UserName)
                    {
                        var dto = new AspNetUserRolesDto();
                        dto.RoleName = roleItem.Name;
                        dto.UserName = usersItem.UserName;
                        result.Items.Add(dto);
                    }
            }

            return result;
        }
    }
}
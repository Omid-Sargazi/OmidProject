using OmidProject.Applications.Contracts.Service;
using OmidProject.Applications.Contracts.UserManagerContracts.Queries;
using OmidProject.Domains.Domain.Identity;
using OmidProject.Frameworks.Contracts.Markers;
using OmidProject.Frameworks.Utilities.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace OmidProject.Applications.Application.SystemMessageHandlers.QueryHandlers;

public class ReadUserQueryHandler : IQueryHandler<ReadUserQuery, ReadUserQueryResponse>
{
    private readonly IApplicantUserService _applicantUserService;
    private readonly UserManager<ApplicationUser> _userManager;

    public ReadUserQueryHandler(UserManager<ApplicationUser> userManager, IApplicantUserService applicantUserService)
    {
        _userManager = userManager;
        _applicantUserService = applicantUserService;
    }

    public async Task<ReadUserQueryResponse> Execute(ReadUserQuery query, CancellationToken cancellationToken)
    {
        var result = new ReadUserQueryResponse();

        if (query.ObjectIsAnyNullOrEmpty())
        {
            var applicantUsers = await _userManager.Users.ToListAsync(cancellationToken);

            result.Items = _applicantUserService.ConvertToDto(applicantUsers);

            return result;
        }

        var userQuery = _userManager.Users;

        if (!query.Email.IsNullOrEmpty())
            userQuery = userQuery.Where(u => u.Email == query.Email);

        if (!query.UserName.IsNullOrEmpty())
            userQuery = userQuery.Where(u => u.UserName == query.UserName);

        var users = await userQuery.ToListAsync(cancellationToken);

        result.Items = _applicantUserService.ConvertToDto(users);

        return result;
    }
}
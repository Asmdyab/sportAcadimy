using MediatR;
using Microsoft.AspNetCore.Identity;
using SportAcademy.Application.Common.Result;

namespace SportAcademy.Application.Queries.AuthQueries.GetRoles;

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, Result<List<string>>>
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public GetRolesQueryHandler(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Result<List<string>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var roles = await Task.FromResult(_roleManager.Roles.Select(r => r.Name!).ToList());
        return Result<List<string>>.Success(roles, nameof(GetRolesQuery));
    }
}

using MediatR;
using Microsoft.AspNetCore.Identity;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.Interfaces;
using SportAcademy.Domain.Entities;

namespace SportAcademy.Application.Queries.UserQueries.GetMyProfile;

public class GetMyProfileQueryHandler : IRequestHandler<GetMyProfileQuery, Result<MyProfileDto>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserContextService _userContext;

    public GetMyProfileQueryHandler(UserManager<AppUser> userManager, IUserContextService userContext)
    {
        _userManager = userManager;
        _userContext = userContext;
    }

    public async Task<Result<MyProfileDto>> Handle(GetMyProfileQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        if (string.IsNullOrEmpty(userId))
            return Result<MyProfileDto>.Failure("GetMyProfile", "User not authenticated", 401);

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return Result<MyProfileDto>.Failure("GetMyProfile", "User not found", 404);

        var roles = await _userManager.GetRolesAsync(user);
        var dto = new MyProfileDto
        {
            Id = user.Id,
            UserName = user.UserName ?? "",
            Email = user.Email ?? "",
            PhoneNumber = user.PhoneNumber,
            Roles = roles.ToList(),
            CreatedAt = user.CreatedAt.ToString("o")
        };

        return Result<MyProfileDto>.Success(dto, nameof(GetMyProfileQuery));
    }
}

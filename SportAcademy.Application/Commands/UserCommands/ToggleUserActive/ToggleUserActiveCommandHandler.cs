using MediatR;
using Microsoft.AspNetCore.Identity;
using SportAcademy.Application.Common.Result;
using SportAcademy.Domain.Entities;

namespace SportAcademy.Application.Commands.UserCommands.ToggleUserActive;

public class ToggleUserActiveCommandHandler : IRequestHandler<ToggleUserActiveCommand, Result<bool>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly string _operationType = "ToggleUserActive";

    public ToggleUserActiveCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Result<bool>> Handle(ToggleUserActiveCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId);
        if (user == null)
            return Result<bool>.Failure(_operationType, "User not found", 404);

        user.IsBanned = !user.IsBanned;
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
            return Result<bool>.Failure(_operationType, "Failed to toggle user status", 500);

        return Result<bool>.Success(true, _operationType);
    }
}

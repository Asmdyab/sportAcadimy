using MediatR;
using Microsoft.AspNetCore.Identity;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.Interfaces;
using SportAcademy.Domain.Entities;

namespace SportAcademy.Application.Commands.AuthCommands.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<bool>>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IUserContextService _userContext;
    private readonly string _operationType = "ChangePassword";

    public ChangePasswordCommandHandler(UserManager<AppUser> userManager, IUserContextService userContext)
    {
        _userManager = userManager;
        _userContext = userContext;
    }

    public async Task<Result<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var userId = _userContext.UserId;
        if (string.IsNullOrEmpty(userId))
            return Result<bool>.Failure(_operationType, "User not authenticated", 401);

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
            return Result<bool>.Failure(_operationType, "User not found", 404);

        var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result<bool>.Failure(_operationType, errors, 400);
        }

        return Result<bool>.Success(true, _operationType);
    }
}

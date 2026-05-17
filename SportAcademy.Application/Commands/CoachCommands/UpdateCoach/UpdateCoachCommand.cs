using MediatR;
using SportAcademy.Application.Common.Result;

namespace SportAcademy.Application.Commands.CoachCommands.UpdateCoach
{
    public record UpdateCoachCommand(
        int Id,
        int? SportId,
        string? SkillLevel
    ) : IRequest<Result<bool>>;
}

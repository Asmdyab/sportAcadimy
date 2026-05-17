using MediatR;
using SportAcademy.Application.Common.Result;

namespace SportAcademy.Application.Commands.SportCommands.AddSkillLevel;

public record AddSkillLevelToSportCommand(int SportId, string Name, string? Description) : IRequest<Result<bool>>;

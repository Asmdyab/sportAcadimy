using MediatR;
using SportAcademy.Application.Common.Result;

namespace SportAcademy.Application.Commands.BranchCommands.DeactivateBranch;

public record DeactivateBranchCommand(int Id) : IRequest<Result<bool>>;

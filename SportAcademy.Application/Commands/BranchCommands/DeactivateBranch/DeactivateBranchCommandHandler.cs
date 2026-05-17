using MediatR;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.Interfaces;

namespace SportAcademy.Application.Commands.BranchCommands.DeactivateBranch;

public class DeactivateBranchCommandHandler : IRequestHandler<DeactivateBranchCommand, Result<bool>>
{
    private readonly IBranchRepository _branchRepository;
    private readonly string _operationType = "DeactivateBranch";

    public DeactivateBranchCommandHandler(IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<Result<bool>> Handle(DeactivateBranchCommand request, CancellationToken cancellationToken)
    {
        var branch = await _branchRepository.GetByIdAsync(request.Id, cancellationToken);
        if (branch == null)
            return Result<bool>.Failure(_operationType, "Branch not found", 404);

        branch.IsActive = false;
        await _branchRepository.UpdateAsync(branch, cancellationToken);
        return Result<bool>.Success(true, _operationType);
    }
}

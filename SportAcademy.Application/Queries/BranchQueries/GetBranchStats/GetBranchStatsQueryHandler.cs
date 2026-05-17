using MediatR;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.BranchDtos;
using SportAcademy.Application.Interfaces;

namespace SportAcademy.Application.Queries.BranchQueries.GetBranchStats;

public class GetBranchStatsQueryHandler : IRequestHandler<GetBranchStatsQuery, Result<BranchStatsDto>>
{
    private readonly IBranchRepository _branchRepository;

    public GetBranchStatsQueryHandler(IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<Result<BranchStatsDto>> Handle(GetBranchStatsQuery request, CancellationToken cancellationToken)
    {
        var stats = await _branchRepository.GetBranchStatsAsync(request.BranchId, cancellationToken);
        return Result<BranchStatsDto>.Success(stats, nameof(GetBranchStatsQuery));
    }
}

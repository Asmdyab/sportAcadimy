using MediatR;
using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.BranchDtos;
using SportAcademy.Application.Interfaces;

namespace SportAcademy.Application.Queries.BranchQueries.SearchBranch;

public class SearchBranchQueryHandler : IRequestHandler<SearchBranchQuery, Result<PagedData<BranchCardDto>>>
{
    private readonly IBranchRepository _branchRepository;

    public SearchBranchQueryHandler(IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<Result<PagedData<BranchCardDto>>> Handle(SearchBranchQuery request, CancellationToken cancellationToken)
    {
        var result = await _branchRepository.SearchAsync(request.Term, request.Page, cancellationToken);
        return Result<PagedData<BranchCardDto>>.Success(result, nameof(SearchBranchQuery));
    }
}

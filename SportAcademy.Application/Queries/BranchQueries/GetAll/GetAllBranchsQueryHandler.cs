using MediatR;
using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.BranchDtos;
using SportAcademy.Application.Interfaces;
using SportAcademy.Domain.Enums;

namespace SportAcademy.Application.Queries.BranchQueries.GetAll
{
    public class GetAllBranchsQueryHandler : IRequestHandler<GetAllBranchesQuery, Result<PagedData<BranchCardDto>>>
    {
        private readonly IBranchRepository _branchRepository;
        private readonly string _operationType = OperationType.GetAll.ToString();

        public GetAllBranchsQueryHandler(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task<Result<PagedData<BranchCardDto>>> Handle(GetAllBranchesQuery request, CancellationToken cancellationToken)
        {
            var result = await _branchRepository.SearchAsync("", request.Page, cancellationToken);
            return Result<PagedData<BranchCardDto>>.Success(result, nameof(GetAllBranchesQuery));
        }
    }
}

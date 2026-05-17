using MediatR;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.BranchDtos;
using SportAcademy.Application.Interfaces;
using SportAcademy.Domain.Enums;

namespace SportAcademy.Application.Queries.BranchQueries.GetDropdown;

public class GetBranchesDropdownQueryHandler : IRequestHandler<GetBranchesDropdownQuery, Result<List<BranchDropDownListDto>>>
{
    private readonly IBranchRepository _branchRepository;
    private readonly string _operationType = OperationType.GetAll.ToString();

    public GetBranchesDropdownQueryHandler(IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    public async Task<Result<List<BranchDropDownListDto>>> Handle(GetBranchesDropdownQuery request, CancellationToken cancellationToken)
    {
        var result = await _branchRepository.GetAllBranchsBase(cancellationToken);
        return Result<List<BranchDropDownListDto>>.Success(result, _operationType);
    }
}

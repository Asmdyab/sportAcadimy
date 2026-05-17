using MediatR;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.BranchDtos;

namespace SportAcademy.Application.Queries.BranchQueries.GetDropdown;

public record GetBranchesDropdownQuery() : IRequest<Result<List<BranchDropDownListDto>>>;

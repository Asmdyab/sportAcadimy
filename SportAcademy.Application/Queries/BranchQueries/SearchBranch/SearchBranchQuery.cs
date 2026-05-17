using MediatR;
using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.BranchDtos;

namespace SportAcademy.Application.Queries.BranchQueries.SearchBranch;

public record SearchBranchQuery(
    string Term,
    PageRequest Page
) : IRequest<Result<PagedData<BranchCardDto>>>;

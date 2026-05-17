using MediatR;
using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.BranchDtos;

namespace SportAcademy.Application.Queries.BranchQueries.GetAll
{
    public record GetAllBranchesQuery : IRequest<Result<PagedData<BranchCardDto>>>, IPaginatedRequest
    {
        public PageRequest Page { get; set; }

        public GetAllBranchesQuery(PageRequest page)
        {
            Page = page;
        }
    }
}

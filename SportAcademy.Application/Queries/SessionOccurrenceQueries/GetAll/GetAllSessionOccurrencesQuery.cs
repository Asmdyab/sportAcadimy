using MediatR;
using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.SessionOccurrenceDtos;

namespace SportAcademy.Application.Queries.SessionOccurrenceQueries.GetAll
{
    public record GetAllSessionOccurrencesQuery : IRequest<Result<PagedData<SessionOccurrenceCardDto>>>, IPaginatedRequest
    {
        public PageRequest Page { get; set; }

        public GetAllSessionOccurrencesQuery(PageRequest page)
        {
            Page = page;
        }
    }
}

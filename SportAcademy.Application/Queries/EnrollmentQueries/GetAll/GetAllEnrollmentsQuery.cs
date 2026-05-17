using MediatR;
using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.EnrollmentDtos;

namespace SportAcademy.Application.Queries.EnrollmentQueries.GetAll
{
    public record GetAllEnrollmentsQuery(PageRequest Page, string? Status, string? PaymentStatus)
        : IRequest<Result<PagedData<EnrollmentCardDto>>>, IPaginatedRequest
    {
        public PageRequest Page { get; set; } = Page;
    }
}

using MediatR;
using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.EnrollmentDtos;

namespace SportAcademy.Application.Queries.EnrollmentQueries.SearchEnrollment;

public record SearchEnrollmentQuery(
    string Term,
    PageRequest Page,
    string? Status,
    string? PaymentStatus
) : IRequest<Result<PagedData<EnrollmentCardDto>>>;

using MediatR;
using SportAcademy.Application.Common.Result;

namespace SportAcademy.Application.Queries.EnrollmentQueries.GetActiveEnrollmentsCount;

public record GetActiveEnrollmentsCountQuery() : IRequest<Result<int>>;

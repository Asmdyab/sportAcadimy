using MediatR;
using SportAcademy.Application.Common.Result;

namespace SportAcademy.Application.Queries.EnrollmentQueries.GetEnrollmentsCount;

public record GetEnrollmentsCountQuery() : IRequest<Result<int>>;

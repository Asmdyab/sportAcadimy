using MediatR;
using SportAcademy.Application.Common.Result;

namespace SportAcademy.Application.Queries.EnrollmentQueries.GetPendingPaymentsCount;

public record GetPendingPaymentsCountQuery() : IRequest<Result<int>>;

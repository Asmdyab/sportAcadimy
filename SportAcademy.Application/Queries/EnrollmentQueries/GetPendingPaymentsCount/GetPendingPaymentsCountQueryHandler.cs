using MediatR;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.Interfaces;

namespace SportAcademy.Application.Queries.EnrollmentQueries.GetPendingPaymentsCount;

public class GetPendingPaymentsCountQueryHandler : IRequestHandler<GetPendingPaymentsCountQuery, Result<int>>
{
    private readonly IEnrollmentRepository _enrollmentRepository;

    public GetPendingPaymentsCountQueryHandler(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<Result<int>> Handle(GetPendingPaymentsCountQuery request, CancellationToken cancellationToken)
    {
        var count = await _enrollmentRepository.GetPendingPaymentCountAsync(cancellationToken);
        return Result<int>.Success(count, nameof(GetPendingPaymentsCountQuery));
    }
}

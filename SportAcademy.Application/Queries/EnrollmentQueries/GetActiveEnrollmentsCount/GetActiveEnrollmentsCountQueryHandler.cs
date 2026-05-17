using MediatR;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.Interfaces;

namespace SportAcademy.Application.Queries.EnrollmentQueries.GetActiveEnrollmentsCount;

public class GetActiveEnrollmentsCountQueryHandler : IRequestHandler<GetActiveEnrollmentsCountQuery, Result<int>>
{
    private readonly IEnrollmentRepository _enrollmentRepository;

    public GetActiveEnrollmentsCountQueryHandler(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<Result<int>> Handle(GetActiveEnrollmentsCountQuery request, CancellationToken cancellationToken)
    {
        var count = await _enrollmentRepository.GetActiveCountAsync(cancellationToken);
        return Result<int>.Success(count, nameof(GetActiveEnrollmentsCountQuery));
    }
}

using MediatR;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.Interfaces;

namespace SportAcademy.Application.Queries.EnrollmentQueries.GetEnrollmentsCount;

public class GetEnrollmentsCountQueryHandler : IRequestHandler<GetEnrollmentsCountQuery, Result<int>>
{
    private readonly IEnrollmentRepository _enrollmentRepository;

    public GetEnrollmentsCountQueryHandler(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<Result<int>> Handle(GetEnrollmentsCountQuery request, CancellationToken cancellationToken)
    {
        var count = await _enrollmentRepository.GetCountAsync(cancellationToken);
        return Result<int>.Success(count, nameof(GetEnrollmentsCountQuery));
    }
}

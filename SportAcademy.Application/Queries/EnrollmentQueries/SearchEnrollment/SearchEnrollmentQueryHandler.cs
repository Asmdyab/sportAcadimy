using MediatR;
using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.EnrollmentDtos;
using SportAcademy.Application.Interfaces;

namespace SportAcademy.Application.Queries.EnrollmentQueries.SearchEnrollment;

public class SearchEnrollmentQueryHandler : IRequestHandler<SearchEnrollmentQuery, Result<PagedData<EnrollmentCardDto>>>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly string _operationType = nameof(SearchEnrollmentQuery);

    public SearchEnrollmentQueryHandler(IEnrollmentRepository enrollmentRepository)
    {
        _enrollmentRepository = enrollmentRepository;
    }

    public async Task<Result<PagedData<EnrollmentCardDto>>> Handle(SearchEnrollmentQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Term))
            return Result<PagedData<EnrollmentCardDto>>.Failure(_operationType, "Search term required");

        if (request.Term.Trim().Length < 2)
            return Result<PagedData<EnrollmentCardDto>>.Failure(_operationType, "Minimum 2 characters");

        var result = await _enrollmentRepository.SearchAsync(
            request.Term, request.Page, request.Status, request.PaymentStatus, cancellationToken);

        return Result<PagedData<EnrollmentCardDto>>.Success(result, _operationType);
    }
}

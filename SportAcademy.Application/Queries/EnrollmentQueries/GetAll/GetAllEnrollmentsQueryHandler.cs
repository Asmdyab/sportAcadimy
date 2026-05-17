using MediatR;
using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.EnrollmentDtos;
using SportAcademy.Application.Interfaces;
using SportAcademy.Domain.Enums;

namespace SportAcademy.Application.Queries.EnrollmentQueries.GetAll
{
    public class GetAllEnrollmentsQueryHandler : IRequestHandler<GetAllEnrollmentsQuery, Result<PagedData<EnrollmentCardDto>>>
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly string _operationType = OperationType.GetAll.ToString();

        public GetAllEnrollmentsQueryHandler(IEnrollmentRepository enrollmentRepository)
        {
            _enrollmentRepository = enrollmentRepository;
        }

        public async Task<Result<PagedData<EnrollmentCardDto>>> Handle(GetAllEnrollmentsQuery request, CancellationToken cancellationToken)
        {
            var result = await _enrollmentRepository.GetAllPaginatedAsync(
                request.Page, request.Status, request.PaymentStatus, cancellationToken);

            return Result<PagedData<EnrollmentCardDto>>.Success(result, _operationType);
        }
    }
}

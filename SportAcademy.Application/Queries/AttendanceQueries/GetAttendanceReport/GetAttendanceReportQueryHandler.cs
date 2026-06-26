using MediatR;
using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.AttendanceDtos;
using SportAcademy.Application.Interfaces;
using SportAcademy.Domain.Enums;

namespace SportAcademy.Application.Queries.AttendanceQueries.GetAttendanceReport
{
    public class GetAttendanceReportQueryHandler
        : IRequestHandler<GetAttendanceReportQuery, Result<PagedData<TraineeAttendanceReportDto>>>
    {
        private readonly IAttendanceRepository _attendanceRepository;
        private readonly string _operationType = OperationType.Get.ToString();

        public GetAttendanceReportQueryHandler(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        public async Task<Result<PagedData<TraineeAttendanceReportDto>>> Handle(
            GetAttendanceReportQuery request,
            CancellationToken cancellationToken)
        {
            var page = PageRequest.Create(request.Page, request.PageSize);

            var reportData = await _attendanceRepository
                .GetAttendanceReportAsync(page, cancellationToken);

            var enrollmentIds = reportData.Items.Select(x => x.EnrollmentId);

            var statusesByEnrollment = await _attendanceRepository
                .GetAttendanceStatusesByEnrollmentsAsync(enrollmentIds, cancellationToken);

            var updatedItems = reportData.Items
                .Select(item =>
                {
                    var statuses = statusesByEnrollment.TryGetValue(item.EnrollmentId, out var s)
                        ? s
                        : [];

                    return item with
                    {
                        ConsecutiveAbsences = CalculateConsecutiveAbsences(statuses)
                    };
                })
                .ToList();

            var result = new PagedData<TraineeAttendanceReportDto>
            {
                Items = updatedItems,
                TotalCount = reportData.TotalCount,
                Page = reportData.Page,
                PageSize = reportData.PageSize
            };

            return Result<PagedData<TraineeAttendanceReportDto>>
                .Success(result, _operationType);
        }

        private static int CalculateConsecutiveAbsences(List<AttendanceStatus> statusesDesc)
        {
            var count = 0;

            foreach (var status in statusesDesc)
            {
                if (status == AttendanceStatus.Absent)
                    count++;
                else
                    break;
            }

            return count;
        }
    }
}
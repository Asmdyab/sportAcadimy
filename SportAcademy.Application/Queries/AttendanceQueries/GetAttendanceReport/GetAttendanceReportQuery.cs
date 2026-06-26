using MediatR;
using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.AttendanceDtos;

namespace SportAcademy.Application.Queries.AttendanceQueries.GetAttendanceReport
{
    public record GetAttendanceReportQuery(
        int? Page,
        int? PageSize
    ) : IRequest<Result<PagedData<TraineeAttendanceReportDto>>>;
}
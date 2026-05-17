using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.DTOs.AttendanceDtos;
using SportAcademy.Domain.Entities;
using SportAcademy.Domain.Enums;

namespace SportAcademy.Application.Interfaces
{
    public interface IAttendanceRepository : IBaseRepository<Attendance, int>
    {
        Task<PagedData<AttendanceDto>> GetAllAsync(PageRequest page, CancellationToken cancellationToken = default);
        Task<int> GetMonthlyAttendanceRate(Month month, CancellationToken ct = default);
        Task<int> GetGlobalAttendanceRate(CancellationToken ct = default);
        Task<(int TotalSessions, int AttendedSessions)> GetAttendanceSummaryAsync(
            int traineeId,
            DateOnly? fromDate,
            DateOnly? toDate,
            CancellationToken cancellationToken
        );
        Task<List<AttendanceRecordDto>> GetBySessionAsync(int sessionOccurrenceId, CancellationToken ct = default);
        Task<Attendance?> GetBySessionAndEnrollmentAsync(int sessionOccurrenceId, int enrollmentId, CancellationToken ct = default);
    }
}

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.DTOs.AttendanceDtos;
using SportAcademy.Application.Interfaces;
using SportAcademy.Domain.Entities;
using SportAcademy.Domain.Enums;
using SportAcademy.Infrastructure.Persistence.DBContext;
using SportAcademy.Infrastructure.Persistence.Extensions.QueryExtensions;

namespace SportAcademy.Infrastructure.Persistence.Repositories
{
    public class AttendanceRepository : BaseRepository<Attendance, int>, IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public AttendanceRepository(ApplicationDbContext context, IMapper mapper)
            : base(context, mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> GetGlobalAttendanceRate(CancellationToken ct = default)
            => await _context.Attendances
                .CountAsync(a => a.AttendanceStatus == AttendanceStatus.Present, ct) * 100 /
                await _context.Attendances.CountAsync(ct);

        public async Task<int> GetMonthlyAttendanceRate(Month month, CancellationToken ct = default)
            => await _context.Attendances
                .Where(a => a.AttendanceDate.Month == (int)month)
                .CountAsync(a => a.AttendanceStatus == AttendanceStatus.Present, ct) * 100 /
                await _context.Attendances.CountAsync(ct);

        public async Task<PagedData<AttendanceDto>> GetAllAsync(
            PageRequest page,
            CancellationToken cancellationToken = default)
            => await _context.Attendances
                .Include(a => a.Enrollment)
                .Include(a => a.SessionOccurrence)
                .ProjectTo<AttendanceDto>(_mapper.ConfigurationProvider)
                .ToPagedDataAsync(page, cancellationToken);

        public async Task<(int TotalSessions, int AttendedSessions)> GetAttendanceSummaryAsync(
            int traineeId,
            DateOnly? fromDate,
            DateOnly? toDate,
            CancellationToken cancellationToken)
        {
            var query = _context.Attendances
                .Include(a => a.Enrollment)
                .Include(a => a.SessionOccurrence)
                .Where(a => a.Enrollment.TraineeId == traineeId);

            if (fromDate.HasValue)
                query = query.Where(a =>
                    DateOnly.FromDateTime(a.SessionOccurrence.StartDateTime) >= fromDate.Value);

            if (toDate.HasValue)
                query = query.Where(a =>
                    DateOnly.FromDateTime(a.SessionOccurrence.StartDateTime) <= toDate.Value);

            var total = await query.CountAsync(cancellationToken);
            var attended = await query
                .CountAsync(a => a.AttendanceStatus == AttendanceStatus.Present, cancellationToken);

            return (total, attended);
        }

        public async Task<PagedData<TraineeAttendanceReportDto>> GetAttendanceReportAsync(
            PageRequest page,
            CancellationToken cancellationToken = default)
        {
            return await _context.TraineeAttendanceReports
                .Select(v => new TraineeAttendanceReportDto(
                    v.TraineeId,
                    v.FirstName,
                    v.LastName,
                    v.GroupId,
                    v.GroupName,
                    v.SportName,
                    v.BranchName,
                    v.SubscriptionStartDate,
                    v.SubscriptionEndDate,
                    v.EnrollmentId,
                    v.IsActive,
                    v.TotalSessions,
                    v.AttendedSessions,
                    v.AbsentSessions,
                    v.AttendanceRate,
                    v.AbsenceRate,
                    0 // ConsecutiveAbsences — يتحسب في الـ Handler
                ))
                .ToPagedDataAsync(page, cancellationToken);
        }

        public async Task<Dictionary<int, List<AttendanceStatus>>> GetAttendanceStatusesByEnrollmentsAsync(
            IEnumerable<int> enrollmentIds,
            CancellationToken cancellationToken = default)
        {
            var records = await _context.Attendances
                .Where(a => enrollmentIds.Contains(a.EnrollmentId))
                .OrderByDescending(a => a.SessionOccurrence.StartDateTime)
                .Select(a => new { a.EnrollmentId, a.AttendanceStatus })
                .ToListAsync(cancellationToken);

            return records
                .GroupBy(a => a.EnrollmentId)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(a => a.AttendanceStatus).ToList()
                );
        }
    }
}
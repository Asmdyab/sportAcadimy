using Microsoft.EntityFrameworkCore;
using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.DTOs.SessionOccurrenceDtos;
using SportAcademy.Application.Interfaces;
using SportAcademy.Domain.Entities;
using SportAcademy.Infrastructure.Persistence.DBContext;
using SportAcademy.Infrastructure.Persistence.Extensions.QueryExtensions;

namespace SportAcademy.Infrastructure.Persistence.Repositories
{
    public class SessionOccurrenceRepository : BaseRepository<SessionOccurrence, int>, ISessionOccurrenceRepository
    {
        private readonly ApplicationDbContext _context;

        public SessionOccurrenceRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PagedData<SessionOccurrenceCardDto>> SearchAsync(string term, PageRequest page, CancellationToken ct = default)
        {
            var query = _context.SessionOccurrences
                .Select(s => new SessionOccurrenceCardDto
                {
                    Id = s.Id,
                    SportName = s.GroupSchedule.TraineeGroup.Coach.Sport.Name,
                    CoachName = s.GroupSchedule.TraineeGroup.Coach.Employee.FirstName + " " + s.GroupSchedule.TraineeGroup.Coach.Employee.LastName,
                    BranchName = s.GroupSchedule.TraineeGroup.Branch.Name,
                    StartTime = s.StartDateTime,
                    DurationInMinutes = s.GroupSchedule.TraineeGroup.DurationInMinutes,
                    TraineesCount = s.GroupSchedule.TraineeGroup.Enrollments.Count(e => e.IsActive && !e.IsDeleted),
                    Date = s.StartDateTime.Date
                });

            if (!string.IsNullOrWhiteSpace(term))
            {
                var t = term.ToLower();
                query = query.Where(s =>
                    s.SportName!.ToLower().Contains(t) ||
                    s.CoachName!.ToLower().Contains(t) ||
                    s.BranchName!.ToLower().Contains(t));
            }

            return await query.ToPagedDataAsync(page, ct);
        }

        public async Task<PagedData<SessionOccurrenceCardDto>> GetByDateAsync(DateTime date, PageRequest page, CancellationToken ct = default)
        {
            var dateStart = date.Date;
            var dateEnd = dateStart.AddDays(1);

            var query = _context.SessionOccurrences
                .Where(s => s.StartDateTime >= dateStart && s.StartDateTime < dateEnd)
                .Select(s => new SessionOccurrenceCardDto
                {
                    Id = s.Id,
                    SportName = s.GroupSchedule.TraineeGroup.Coach.Sport.Name,
                    CoachName = s.GroupSchedule.TraineeGroup.Coach.Employee.FirstName + " " + s.GroupSchedule.TraineeGroup.Coach.Employee.LastName,
                    BranchName = s.GroupSchedule.TraineeGroup.Branch.Name,
                    StartTime = s.StartDateTime,
                    DurationInMinutes = s.GroupSchedule.TraineeGroup.DurationInMinutes,
                    TraineesCount = s.GroupSchedule.TraineeGroup.Enrollments.Count(e => e.IsActive && !e.IsDeleted),
                    Date = s.StartDateTime.Date
                });

            return await query.ToPagedDataAsync(page, ct);
        }

        public async Task<int> GetCountAsync(CancellationToken ct = default)
            => await _context.SessionOccurrences.CountAsync(ct);

        public async Task<bool> ExistsByScheduleAndDateTimeAsync(int groupScheduleId, DateTime startDateTime, CancellationToken ct = default)
            => await _context.SessionOccurrences
                .AnyAsync(s => s.GroupScheduleId == groupScheduleId && s.StartDateTime == startDateTime, ct);
    }
}

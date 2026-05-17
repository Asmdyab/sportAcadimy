using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.DTOs.GroupScheduleDtos;
using SportAcademy.Application.DTOs.TraineeGroupDtos;
using SportAcademy.Application.Interfaces;
using SportAcademy.Domain.Entities;
using SportAcademy.Infrastructure.Persistence.DBContext;
using SportAcademy.Infrastructure.Persistence.Extensions.QueryExtensions;

namespace SportAcademy.Infrastructure.Persistence.Repositories
{
    public class TraineeGroupRepository : BaseRepository<TraineeGroup, int>, ITraineeGroupRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TraineeGroupRepository(ApplicationDbContext context, IMapper mapper)
            : base(context, mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<PagedData<ListTraineeGroupDto>> GetAllOfSpecificDayAsync(PageRequest page, DateTime day, CancellationToken cancellationToken = default)
            => await _context.TraineeGroups
                .AsNoTracking()
                .Where(tg => tg.GroupSchedules.Any(gs => gs.Day == day.DayOfWeek))
                .Select(tg => new ListTraineeGroupDto
                {
                    Id = tg.Id,
                    Name = tg.Name,
                    SportName = tg.Coach.Sport.Name,
                    CoachName = tg.Coach.Employee.FirstName,
                    BranchName = tg.Branch.Name,
                    DurationInMinutes = tg.DurationInMinutes,
                    TraineesCount = tg.Enrollments.Count,
                    Schedules = tg.GroupSchedules.Select(gs => new GroupScheduleDto
                    {
                        DayOfWeek = gs.Day,
                        StartTime = gs.StartTime
                    }).ToList()
                })
                .ToPagedDataAsync(page, cancellationToken);

        public async Task<PagedData<TraineeGroupCardDto>> GetAllAsCardAsync(PageRequest page, CancellationToken cancellationToken = default)
            => await _context.TraineeGroups
                .AsNoTracking()
                .ProjectTo<TraineeGroupCardDto>(_mapper.ConfigurationProvider)
                .ToPagedDataAsync(page, cancellationToken);

        public async Task<TraineeGroupDetailDto?> GetDetailsByIdAsync(int id, CancellationToken cancellationToken = default)
            => await _context.TraineeGroups
                .AsNoTracking()
                .Where(tg => tg.Id == id)
                .ProjectTo<TraineeGroupDetailDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

        public async Task<TraineeGroup?> GetByIdWithSchedulesAsync(int id, CancellationToken cancellationToken = default)
            => await _context.TraineeGroups
                .Include(tg => tg.GroupSchedules)
                .FirstOrDefaultAsync(tg => tg.Id == id, cancellationToken);

        public async Task<int> GetCountAsync(CancellationToken cancellation = default)
            => await _context.TraineeGroups.CountAsync(cancellation);

        public async Task<PagedData<ListTraineeGroupDto>> SearchAsync(string term, PageRequest page, CancellationToken cancellationToken = default)
        {
            var query = _context.TraineeGroups
                .Where(g => g.Name.Contains(term))
                .Select(g => new ListTraineeGroupDto
                {
                    Id = g.Id,
                    Name = g.Name,
                    SportName = g.Coach.Sport.Name,
                    CoachName = g.Coach.Employee.FirstName + " " + g.Coach.Employee.LastName,
                    BranchName = g.Branch.Name,
                    DurationInMinutes = g.DurationInMinutes,
                    TraineesCount = g.Enrollments.Count(e => !e.IsDeleted),
                    Schedules = g.GroupSchedules.Select(gs => new GroupScheduleDto
                    {
                        DayOfWeek = gs.Day,
                        StartTime = gs.StartTime
                    }).ToList()
                });

            var total = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip(page.Skip)
                .Take(page.PageSize)
                .ToListAsync(cancellationToken);

            return new PagedData<ListTraineeGroupDto>
            {
                Items = items,
                TotalCount = total,
                Page = page.Page,
                PageSize = page.PageSize
            };
        }
    }

}


using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.DTOs.SessionOccurrenceDtos;
using SportAcademy.Domain.Entities;

namespace SportAcademy.Application.Interfaces
{
    public interface ISessionOccurrenceRepository : IBaseRepository<SessionOccurrence, int>
    {
        Task<PagedData<SessionOccurrenceCardDto>> SearchAsync(string term, PageRequest page, CancellationToken ct = default);
        Task<PagedData<SessionOccurrenceCardDto>> GetByDateAsync(DateTime date, PageRequest page, CancellationToken ct = default);
        Task<int> GetCountAsync(CancellationToken ct = default);
        Task<bool> ExistsByScheduleAndDateTimeAsync(int groupScheduleId, DateTime startDateTime, CancellationToken ct = default);
    }
}

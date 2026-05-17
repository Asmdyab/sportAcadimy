using MediatR;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.Interfaces;
using SportAcademy.Domain.Entities;
using SportAcademy.Domain.Enums;

namespace SportAcademy.Application.Commands.SessionOccurrenceCommands.GenerateOccurrences;

public class GenerateSessionOccurrencesCommandHandler : IRequestHandler<GenerateSessionOccurrencesCommand, Result<int>>
{
    private readonly ITraineeGroupRepository _groupRepository;
    private readonly ISessionOccurrenceRepository _occurrenceRepository;
    private readonly string _operationType = OperationType.Add.ToString();

    public GenerateSessionOccurrencesCommandHandler(
        ITraineeGroupRepository groupRepository,
        ISessionOccurrenceRepository occurrenceRepository)
    {
        _groupRepository = groupRepository;
        _occurrenceRepository = occurrenceRepository;
    }

    public async Task<Result<int>> Handle(GenerateSessionOccurrencesCommand request, CancellationToken cancellationToken)
    {
        var group = await _groupRepository.GetByIdWithSchedulesAsync(request.TraineeGroupId, cancellationToken);

        if (group == null)
            return Result<int>.Failure(_operationType, "Trainee group not found", 404);

        var schedules = request.GroupScheduleId.HasValue
            ? group.GroupSchedules.Where(s => s.Id == request.GroupScheduleId.Value).ToList()
            : group.GroupSchedules.ToList();

        if (schedules.Count == 0)
            return Result<int>.Failure(_operationType, "No schedules found for this group", 404);

        var startDate = request.StartDate ?? DateTime.UtcNow;
        var generatedCount = 0;

        for (int day = 0; day < request.DurationInDays; day++)
        {
            var currentDate = startDate.AddDays(day);
            var dayOfWeek = currentDate.DayOfWeek;

            var matchingSchedules = schedules.Where(s => s.Day == dayOfWeek).ToList();
            foreach (var schedule in matchingSchedules)
            {
                var startDateTime = currentDate.Date + schedule.StartTime.ToTimeSpan();

                var exists = await _occurrenceRepository.ExistsByScheduleAndDateTimeAsync(schedule.Id, startDateTime, cancellationToken);
                if (!exists)
                {
                    await _occurrenceRepository.AddAsync(new SessionOccurrence
                    {
                        GroupScheduleId = schedule.Id,
                        StartDateTime = startDateTime,
                        Status = SessionStatus.Scheduled
                    }, cancellationToken);
                    generatedCount++;
                }
            }
        }

        return Result<int>.Success(generatedCount, _operationType);
    }
}

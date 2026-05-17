using MediatR;
using SportAcademy.Application.Common.Result;

namespace SportAcademy.Application.Commands.SessionOccurrenceCommands.GenerateOccurrences;

public record GenerateSessionOccurrencesCommand(
    int TraineeGroupId,
    int DurationInDays,
    int? GroupScheduleId,
    DateTime? StartDate
) : IRequest<Result<int>>;

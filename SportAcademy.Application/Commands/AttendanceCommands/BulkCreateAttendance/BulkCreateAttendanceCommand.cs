using MediatR;
using SportAcademy.Application.Common.Result;

namespace SportAcademy.Application.Commands.AttendanceCommands.BulkCreateAttendance;

public record BulkCreateAttendanceCommand(List<SingleAttendanceRecord> Records) : IRequest<Result<bool>>;

public class SingleAttendanceRecord
{
    public int SessionOccurrenceId { get; set; }
    public int TraineeId { get; set; }
    public string Status { get; set; } = "Absent";
    public DateTime? CheckInTime { get; set; }
}

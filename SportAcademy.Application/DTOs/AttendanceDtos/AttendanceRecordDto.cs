namespace SportAcademy.Application.DTOs.AttendanceDtos;

public class AttendanceRecordDto
{
    public int Id { get; set; }
    public int TraineeId { get; set; }
    public string? TraineeName { get; set; }
    public string? CheckInTime { get; set; }
    public string Status { get; set; } = "Absent";
}

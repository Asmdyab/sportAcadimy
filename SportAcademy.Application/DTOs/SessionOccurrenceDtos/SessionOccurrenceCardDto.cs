namespace SportAcademy.Application.DTOs.SessionOccurrenceDtos;

public class SessionOccurrenceCardDto
{
    public int Id { get; set; }
    public string? SportName { get; set; }
    public string? CoachName { get; set; }
    public string? BranchName { get; set; }
    public DateTime StartTime { get; set; }
    public int DurationInMinutes { get; set; }
    public int TraineesCount { get; set; }
    public DateTime Date { get; set; }
}

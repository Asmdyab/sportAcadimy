namespace SportAcademy.Application.DTOs.EnrollmentDtos;

public record EnrollmentCardDto
{
    public int Id { get; init; }
    public string TraineeName { get; init; } = string.Empty;
    public string? TraineeEmail { get; init; }
    public string Sport { get; init; } = string.Empty;
    public string? Program { get; init; }
    public string? Branch { get; init; }
    public string? CoachName { get; init; }
    public DateTime? EnrollmentDate { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public decimal? MonthlyFee { get; init; }
    public string? PaymentStatus { get; init; }
    public string Status { get; init; } = string.Empty;
    public int? SessionsCompleted { get; init; }
    public int? TotalSessions { get; init; }
}

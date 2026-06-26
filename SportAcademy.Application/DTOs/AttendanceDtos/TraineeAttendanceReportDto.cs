namespace SportAcademy.Application.DTOs.AttendanceDtos
{
    public record TraineeAttendanceReportDto(
        int TraineeId,
        string FirstName,
        string LastName,

        int GroupId,
        string GroupName,
        string SportName,
        string BranchName,

        DateOnly SubscriptionStartDate,
        DateOnly SubscriptionEndDate,

        int EnrollmentId,
        bool IsActive,

        int TotalSessions,
        int AttendedSessions,
        int AbsentSessions,

        decimal AttendanceRate,
        decimal AbsenceRate,

        // بيتحسب بـ C# في الـ Handler — مش موجود في الـ View
        int ConsecutiveAbsences
    );
}
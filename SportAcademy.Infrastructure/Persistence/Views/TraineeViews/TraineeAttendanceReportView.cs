using SportAcademy.Infrastructure.Persistence.Views.Interfaces;

namespace SportAcademy.Infrastructure.Persistence.Views.TraineeViews
{
    public class TraineeAttendanceReportView : IModelView
    {
        public int TraineeId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;

        public int GroupId { get; set; }
        public string GroupName { get; set; } = null!;

        public string SportName { get; set; } = null!;
        public string BranchName { get; set; } = null!;

        public DateOnly SubscriptionStartDate { get; set; }
        public DateOnly SubscriptionEndDate { get; set; }

        public int EnrollmentId { get; set; }
        public bool IsActive { get; set; }

        public int TotalSessions { get; set; }
        public int AttendedSessions { get; set; }
        public int AbsentSessions { get; set; }

        public decimal AttendanceRate { get; set; }
        public decimal AbsenceRate { get; set; }
    }
}
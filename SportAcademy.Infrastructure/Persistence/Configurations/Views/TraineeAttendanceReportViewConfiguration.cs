using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SportAcademy.Infrastructure.Persistence.Views.TraineeViews;

namespace SportAcademy.Infrastructure.Persistence.Configurations.Views;

public class TraineeAttendanceReportViewConfiguration
    : IEntityTypeConfiguration<TraineeAttendanceReportView>
{
    public void Configure(EntityTypeBuilder<TraineeAttendanceReportView> builder)
    {
        builder.ToView("vw_TraineeAttendanceReport");

        builder.HasNoKey();

        builder.Property(x => x.TraineeId);
        builder.Property(x => x.FirstName);
        builder.Property(x => x.LastName);

        builder.Property(x => x.GroupId);
        builder.Property(x => x.GroupName);

        builder.Property(x => x.SportName);
        builder.Property(x => x.BranchName);

        builder.Property(x => x.SubscriptionStartDate);
        builder.Property(x => x.SubscriptionEndDate);

        builder.Property(x => x.EnrollmentId);
        builder.Property(x => x.IsActive);

        builder.Property(x => x.TotalSessions);
        builder.Property(x => x.AttendedSessions);
        builder.Property(x => x.AbsentSessions);

        builder.Property(x => x.AttendanceRate)
               .HasColumnType("decimal(5,2)");

        builder.Property(x => x.AbsenceRate)
               .HasColumnType("decimal(5,2)");
    }
}
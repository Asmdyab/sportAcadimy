using MediatR;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.Interfaces;
using SportAcademy.Domain.Entities;
using SportAcademy.Domain.Enums;

namespace SportAcademy.Application.Commands.AttendanceCommands.BulkCreateAttendance;

public class BulkCreateAttendanceCommandHandler : IRequestHandler<BulkCreateAttendanceCommand, Result<bool>>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly IAttendanceRepository _attendanceRepository;
    private readonly string _operationType = OperationType.Add.ToString();

    public BulkCreateAttendanceCommandHandler(
        IEnrollmentRepository enrollmentRepository,
        IAttendanceRepository attendanceRepository)
    {
        _enrollmentRepository = enrollmentRepository;
        _attendanceRepository = attendanceRepository;
    }

    public async Task<Result<bool>> Handle(BulkCreateAttendanceCommand request, CancellationToken cancellationToken)
    {
        foreach (var record in request.Records)
        {
            var enrollment = await _enrollmentRepository.GetFirstByTraineeIdAsync(record.TraineeId, cancellationToken);
            if (enrollment == null) continue;

            var existing = await _attendanceRepository.GetBySessionAndEnrollmentAsync(
                record.SessionOccurrenceId, enrollment.Id, cancellationToken);

            var status = Enum.Parse<AttendanceStatus>(record.Status);

            if (existing != null)
            {
                existing.AttendanceStatus = status;
                if (record.CheckInTime.HasValue)
                    existing.CheckInTime = TimeOnly.FromDateTime(record.CheckInTime.Value);
                await _attendanceRepository.UpdateAsync(existing, cancellationToken);
            }
            else
            {
                await _attendanceRepository.AddAsync(new Attendance
                {
                    SessionOccurrenceId = record.SessionOccurrenceId,
                    EnrollmentId = enrollment.Id,
                    AttendanceStatus = status,
                    AttendanceDate = DateTime.UtcNow,
                    CheckInTime = record.CheckInTime.HasValue ? TimeOnly.FromDateTime(record.CheckInTime.Value) : default,
                    CoachNote = ""
                }, cancellationToken);
            }
        }

        return Result<bool>.Success(true, _operationType);
    }
}

using MediatR;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.AttendanceDtos;

namespace SportAcademy.Application.Queries.AttendanceQueries.GetAttendanceBySession;

public record GetAttendanceBySessionQuery(int SessionOccurrenceId) : IRequest<Result<List<AttendanceRecordDto>>>;

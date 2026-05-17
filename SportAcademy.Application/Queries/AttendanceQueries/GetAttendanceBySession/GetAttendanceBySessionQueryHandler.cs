using MediatR;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.AttendanceDtos;
using SportAcademy.Application.Interfaces;
using SportAcademy.Domain.Enums;

namespace SportAcademy.Application.Queries.AttendanceQueries.GetAttendanceBySession;

public class GetAttendanceBySessionQueryHandler : IRequestHandler<GetAttendanceBySessionQuery, Result<List<AttendanceRecordDto>>>
{
    private readonly IAttendanceRepository _repository;
    private readonly string _operationType = OperationType.GetAll.ToString();

    public GetAttendanceBySessionQueryHandler(IAttendanceRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<List<AttendanceRecordDto>>> Handle(GetAttendanceBySessionQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetBySessionAsync(request.SessionOccurrenceId, cancellationToken);
        return Result<List<AttendanceRecordDto>>.Success(result, _operationType);
    }
}

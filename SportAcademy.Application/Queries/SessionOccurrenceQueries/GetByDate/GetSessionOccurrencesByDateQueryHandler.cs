using MediatR;
using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.SessionOccurrenceDtos;
using SportAcademy.Application.Interfaces;
using SportAcademy.Domain.Enums;

namespace SportAcademy.Application.Queries.SessionOccurrenceQueries.GetByDate;

public class GetSessionOccurrencesByDateQueryHandler : IRequestHandler<GetSessionOccurrencesByDateQuery, Result<PagedData<SessionOccurrenceCardDto>>>
{
    private readonly ISessionOccurrenceRepository _repository;
    private readonly string _operationType = OperationType.GetAll.ToString();

    public GetSessionOccurrencesByDateQueryHandler(ISessionOccurrenceRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<PagedData<SessionOccurrenceCardDto>>> Handle(GetSessionOccurrencesByDateQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetByDateAsync(request.Date, request.Page, cancellationToken);
        return Result<PagedData<SessionOccurrenceCardDto>>.Success(result, _operationType);
    }
}

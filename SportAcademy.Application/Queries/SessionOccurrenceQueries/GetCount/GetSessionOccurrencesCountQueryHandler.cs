using MediatR;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.Interfaces;
using SportAcademy.Domain.Enums;

namespace SportAcademy.Application.Queries.SessionOccurrenceQueries.GetCount;

public class GetSessionOccurrencesCountQueryHandler : IRequestHandler<GetSessionOccurrencesCountQuery, Result<int>>
{
    private readonly ISessionOccurrenceRepository _repository;
    private readonly string _operationType = OperationType.GetAll.ToString();

    public GetSessionOccurrencesCountQueryHandler(ISessionOccurrenceRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<int>> Handle(GetSessionOccurrencesCountQuery request, CancellationToken cancellationToken)
    {
        var count = await _repository.GetCountAsync(cancellationToken);
        return Result<int>.Success(count, _operationType);
    }
}

using MediatR;
using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.SessionOccurrenceDtos;
using SportAcademy.Application.Interfaces;
using SportAcademy.Domain.Enums;

namespace SportAcademy.Application.Queries.SessionOccurrenceQueries.SearchSessionOccurrence;

public class SearchSessionOccurrenceQueryHandler : IRequestHandler<SearchSessionOccurrenceQuery, Result<PagedData<SessionOccurrenceCardDto>>>
{
    private readonly ISessionOccurrenceRepository _repository;
    private readonly string _operationType = OperationType.GetAll.ToString();

    public SearchSessionOccurrenceQueryHandler(ISessionOccurrenceRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<PagedData<SessionOccurrenceCardDto>>> Handle(SearchSessionOccurrenceQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.SearchAsync(request.Term, request.Page, cancellationToken);
        return Result<PagedData<SessionOccurrenceCardDto>>.Success(result, _operationType);
    }
}

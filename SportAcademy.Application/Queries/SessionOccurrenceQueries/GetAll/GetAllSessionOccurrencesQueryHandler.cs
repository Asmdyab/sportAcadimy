using MediatR;
using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.SessionOccurrenceDtos;
using SportAcademy.Application.Interfaces;
using SportAcademy.Domain.Enums;

namespace SportAcademy.Application.Queries.SessionOccurrenceQueries.GetAll
{
    public class GetAllSessionOccurrencesQueryHandler : IRequestHandler<GetAllSessionOccurrencesQuery, Result<PagedData<SessionOccurrenceCardDto>>>
    {
        private readonly ISessionOccurrenceRepository _repository;
        private readonly string _operationType = OperationType.GetAll.ToString();

        public GetAllSessionOccurrencesQueryHandler(ISessionOccurrenceRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<PagedData<SessionOccurrenceCardDto>>> Handle(GetAllSessionOccurrencesQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.SearchAsync("", request.Page, cancellationToken);
            return Result<PagedData<SessionOccurrenceCardDto>>.Success(result, _operationType);
        }
    }
}

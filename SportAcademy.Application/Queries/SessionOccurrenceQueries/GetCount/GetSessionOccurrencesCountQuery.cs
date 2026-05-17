using MediatR;
using SportAcademy.Application.Common.Result;

namespace SportAcademy.Application.Queries.SessionOccurrenceQueries.GetCount;

public record GetSessionOccurrencesCountQuery() : IRequest<Result<int>>;

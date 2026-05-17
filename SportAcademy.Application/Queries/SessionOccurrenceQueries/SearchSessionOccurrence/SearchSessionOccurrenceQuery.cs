using MediatR;
using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.SessionOccurrenceDtos;

namespace SportAcademy.Application.Queries.SessionOccurrenceQueries.SearchSessionOccurrence;

public record SearchSessionOccurrenceQuery(string Term, PageRequest Page) : IRequest<Result<PagedData<SessionOccurrenceCardDto>>>;

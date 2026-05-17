using MediatR;
using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.SessionOccurrenceDtos;

namespace SportAcademy.Application.Queries.SessionOccurrenceQueries.GetByDate;

public record GetSessionOccurrencesByDateQuery(DateTime Date, PageRequest Page) : IRequest<Result<PagedData<SessionOccurrenceCardDto>>>;

using MediatR;
using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.TraineeGroupDtos;

namespace SportAcademy.Application.Queries.TraineeGroupQueries.SearchTraineeGroup;

public record SearchTraineeGroupQuery(string Term, PageRequest Page) : IRequest<Result<PagedData<ListTraineeGroupDto>>>;

using MediatR;
using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.TraineeGroupDtos;
using SportAcademy.Application.Interfaces;

namespace SportAcademy.Application.Queries.TraineeGroupQueries.SearchTraineeGroup;

public class SearchTraineeGroupQueryHandler : IRequestHandler<SearchTraineeGroupQuery, Result<PagedData<ListTraineeGroupDto>>>
{
    private readonly ITraineeGroupRepository _traineeGroupRepository;

    public SearchTraineeGroupQueryHandler(ITraineeGroupRepository traineeGroupRepository)
    {
        _traineeGroupRepository = traineeGroupRepository;
    }

    public async Task<Result<PagedData<ListTraineeGroupDto>>> Handle(SearchTraineeGroupQuery request, CancellationToken cancellationToken)
    {
        var result = await _traineeGroupRepository.SearchAsync(request.Term, request.Page, cancellationToken);
        return Result<PagedData<ListTraineeGroupDto>>.Success(result, nameof(SearchTraineeGroupQuery));
    }
}

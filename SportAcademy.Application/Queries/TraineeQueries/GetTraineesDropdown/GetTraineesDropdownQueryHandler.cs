using MediatR;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.TraineeDtos;
using SportAcademy.Application.Interfaces;

namespace SportAcademy.Application.Queries.TraineeQueries.GetTraineesDropdown;

public class GetTraineesDropdownQueryHandler : IRequestHandler<GetTraineesDropdownQuery, Result<List<TraineeDropdownDto>>>
{
    private readonly ITraineeRepository _traineeRepository;

    public GetTraineesDropdownQueryHandler(ITraineeRepository traineeRepository)
    {
        _traineeRepository = traineeRepository;
    }

    public async Task<Result<List<TraineeDropdownDto>>> Handle(GetTraineesDropdownQuery request, CancellationToken cancellationToken)
    {
        var trainees = await _traineeRepository.GetDropdownAsync(cancellationToken);
        return Result<List<TraineeDropdownDto>>.Success(trainees, nameof(GetTraineesDropdownQuery));
    }
}

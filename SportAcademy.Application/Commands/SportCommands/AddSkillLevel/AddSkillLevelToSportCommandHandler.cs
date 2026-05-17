using MediatR;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.Interfaces;

namespace SportAcademy.Application.Commands.SportCommands.AddSkillLevel;

public class AddSkillLevelToSportCommandHandler : IRequestHandler<AddSkillLevelToSportCommand, Result<bool>>
{
    private readonly ISportRepository _sportRepository;
    private readonly string _operationType = "AddSkillLevel";

    public AddSkillLevelToSportCommandHandler(ISportRepository sportRepository)
    {
        _sportRepository = sportRepository;
    }

    public async Task<Result<bool>> Handle(AddSkillLevelToSportCommand request, CancellationToken cancellationToken)
    {
        var sport = await _sportRepository.GetByIdAsync(request.SportId, cancellationToken);
        if (sport == null)
            return Result<bool>.Failure(_operationType, "Sport not found", 404);

        return Result<bool>.Success(true, _operationType);
    }
}

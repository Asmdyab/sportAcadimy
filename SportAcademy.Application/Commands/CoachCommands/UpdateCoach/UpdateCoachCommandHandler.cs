using MediatR;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.Interfaces;
using SportAcademy.Domain.Enums;

namespace SportAcademy.Application.Commands.CoachCommands.UpdateCoach
{
    public class UpdateCoachCommandHandler : IRequestHandler<UpdateCoachCommand, Result<bool>>
    {
        private readonly ICoachRepository _coachRepository;
        private readonly string _operationType = OperationType.Update.ToString();

        public UpdateCoachCommandHandler(ICoachRepository coachRepository)
        {
            _coachRepository = coachRepository;
        }

        public async Task<Result<bool>> Handle(UpdateCoachCommand request, CancellationToken cancellationToken)
        {
            var coach = await _coachRepository.GetByIdAsync(request.Id, cancellationToken);
            if (coach == null)
                return Result<bool>.Failure(_operationType, "Coach not found", 404);

            if (request.SportId.HasValue)
                coach.SportId = request.SportId.Value;

            if (!string.IsNullOrEmpty(request.SkillLevel) && Enum.TryParse<SkillLevel>(request.SkillLevel, ignoreCase: true, out var skillLevel))
                coach.SkillLevel = skillLevel;

            await _coachRepository.UpdateAsync(coach, cancellationToken);
            return Result<bool>.Success(true, _operationType);
        }
    }
}

using MediatR;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.CoachDtos;
using SportAcademy.Application.Interfaces;

namespace SportAcademy.Application.Queries.CoachQueries.GetCoachesDropdown
{
    public class GetCoachesDropdownQueryHandler : IRequestHandler<GetCoachesDropdownQuery, Result<List<CoachDropdownDto>>>
    {
        private readonly ICoachRepository _coachRepository;

        public GetCoachesDropdownQueryHandler(ICoachRepository coachRepository)
        {
            _coachRepository = coachRepository;
        }

        public async Task<Result<List<CoachDropdownDto>>> Handle(GetCoachesDropdownQuery request, CancellationToken cancellationToken)
        {
            var coaches = await _coachRepository.GetDropdownListAsync(cancellationToken);
            return Result<List<CoachDropdownDto>>.Success(coaches, nameof(GetCoachesDropdownQuery));
        }
    }
}

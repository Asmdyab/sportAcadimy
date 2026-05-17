using MediatR;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.CoachDtos;

namespace SportAcademy.Application.Queries.CoachQueries.GetCoachesDropdown
{
    public record GetCoachesDropdownQuery() : IRequest<Result<List<CoachDropdownDto>>>;
}

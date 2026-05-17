using MediatR;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.TraineeDtos;

namespace SportAcademy.Application.Queries.TraineeQueries.GetTraineesDropdown;

public record GetTraineesDropdownQuery() : IRequest<Result<List<TraineeDropdownDto>>>;

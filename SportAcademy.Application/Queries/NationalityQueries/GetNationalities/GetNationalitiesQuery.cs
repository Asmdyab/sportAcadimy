using MediatR;
using SportAcademy.Application.Common.Result;

namespace SportAcademy.Application.Queries.NationalityQueries.GetNationalities;

public record GetNationalitiesQuery() : IRequest<Result<List<NationalityDto>>>;

public class NationalityDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

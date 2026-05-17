using MediatR;
using SportAcademy.Application.Common.Result;

namespace SportAcademy.Application.Queries.NationalityQueries.GetNationalities;

public class GetNationalitiesQueryHandler : IRequestHandler<GetNationalitiesQuery, Result<List<NationalityDto>>>
{
    public async Task<Result<List<NationalityDto>>> Handle(GetNationalitiesQuery request, CancellationToken cancellationToken)
    {
        var nationalities = System.Enum.GetValues<Domain.Enums.Nationality>()
            .Select((n, i) => new NationalityDto { Id = i + 1, Name = n.ToString() })
            .ToList();

        return Result<List<NationalityDto>>.Success(nationalities, nameof(GetNationalitiesQuery));
    }
}

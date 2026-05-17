using MediatR;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.SubscriptionDetailsDtos;
using SportAcademy.Application.Interfaces;

namespace SportAcademy.Application.Queries.SubscriptionDetailsQueries.GetSubDetailsDropdown;

public class GetSubDetailsDropdownQueryHandler : IRequestHandler<GetSubDetailsDropdownQuery, Result<List<SubDetailsDropdownDto>>>
{
    private readonly ISubscriptionDetailsRepository _subscriptionDetailsRepository;

    public GetSubDetailsDropdownQueryHandler(ISubscriptionDetailsRepository subscriptionDetailsRepository)
    {
        _subscriptionDetailsRepository = subscriptionDetailsRepository;
    }

    public async Task<Result<List<SubDetailsDropdownDto>>> Handle(GetSubDetailsDropdownQuery request, CancellationToken cancellationToken)
    {
        var items = await _subscriptionDetailsRepository.GetDropdownAsync(cancellationToken);
        return Result<List<SubDetailsDropdownDto>>.Success(items, nameof(GetSubDetailsDropdownQuery));
    }
}

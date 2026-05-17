using MediatR;
using SportAcademy.Application.Common.Result;
using SportAcademy.Application.DTOs.SubscriptionDetailsDtos;

namespace SportAcademy.Application.Queries.SubscriptionDetailsQueries.GetSubDetailsDropdown;

public record GetSubDetailsDropdownQuery() : IRequest<Result<List<SubDetailsDropdownDto>>>;

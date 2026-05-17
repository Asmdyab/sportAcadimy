using MediatR;
using SportAcademy.Application.Common.Result;

namespace SportAcademy.Application.Queries.AuthQueries.GetRoles;

public record GetRolesQuery() : IRequest<Result<List<string>>>;

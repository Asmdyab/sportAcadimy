using MediatR;
using SportAcademy.Application.Common.Result;

namespace SportAcademy.Application.Queries.UserQueries.GetMyProfile;

public record GetMyProfileQuery() : IRequest<Result<MyProfileDto>>;

public class MyProfileDto
{
    public string Id { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public List<string>? Roles { get; set; }
    public string? CreatedAt { get; set; }
}

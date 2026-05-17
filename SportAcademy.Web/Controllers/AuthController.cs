using MediatR;
using Microsoft.AspNetCore.Mvc;
using SportAcademy.Application.Commands.AuthCommands.ChangePassword;
using SportAcademy.Application.Commands.AuthCommands.Login;
using SportAcademy.Application.Commands.AuthCommands.Register;
using SportAcademy.Application.Queries.AuthQueries.GetRoles;

namespace SportAcademy.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(RegisterCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetRolesQuery(), ct);
            return Ok(result);
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportAcademy.Application.Commands.AttendanceCommands.CreateAttendance;
using SportAcademy.Application.Commands.AttendanceCommands.DeleteAttendance;
using SportAcademy.Application.Commands.AttendanceCommands.UpdateAttendance;
using SportAcademy.Application.Commands.SessionOccurrenceCommands.CreateSessionOccurrence;
using SportAcademy.Application.Commands.SessionOccurrenceCommands.DeleteSessionOccurence;
using SportAcademy.Application.Commands.SessionOccurrenceCommands.GenerateOccurrences;
using SportAcademy.Application.Commands.SessionOccurrenceCommands.UpdateSessionOccurrence;
using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.Queries.AttendanceQueries.GetById;
using SportAcademy.Application.Queries.BranchQueries.GetAll;
using SportAcademy.Application.Queries.SessionOccurrenceQueries.GetAll;
using SportAcademy.Application.Queries.SessionOccurrenceQueries.GetById;
using SportAcademy.Application.Queries.SessionOccurrenceQueries.GetCount;
using SportAcademy.Application.Queries.SessionOccurrenceQueries.GetByDate;
using SportAcademy.Application.Queries.SessionOccurrenceQueries.SearchSessionOccurrence;

namespace SportAcademy.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SessionOccurrenceController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SessionOccurrenceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSessionOccurrenceCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] int? page,
            [FromQuery] int? pageSize,
            CancellationToken ct)
        {
            var result = await _mediator.Send(
                new GetAllSessionOccurrencesQuery(PageRequest.Create(page, pageSize)), ct);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetSessionOccurrenceByIdQuery(id));
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSessionOccurrenceCommand command,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteSessionOccurrenceCommand(id));
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] string searchTerm,
            [FromQuery] int? page,
            [FromQuery] int? pageSize,
            CancellationToken ct)
        {
            var result = await _mediator.Send(
                new SearchSessionOccurrenceQuery(searchTerm, PageRequest.Create(page, pageSize)), ct);
            return Ok(result);
        }

        [HttpGet("by-date")]
        public async Task<IActionResult> GetByDate(
            [FromQuery] DateTime date,
            [FromQuery] int? page,
            [FromQuery] int? pageSize,
            CancellationToken ct)
        {
            var result = await _mediator.Send(
                new GetSessionOccurrencesByDateQuery(date, PageRequest.Create(page, pageSize)), ct);
            return Ok(result);
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCount(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetSessionOccurrencesCountQuery(), ct);
            return Ok(result);
        }

        [HttpPost("generate")]
        public async Task<IActionResult> Generate(GenerateSessionOccurrencesCommand command, CancellationToken ct)
        {
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }
    }
}

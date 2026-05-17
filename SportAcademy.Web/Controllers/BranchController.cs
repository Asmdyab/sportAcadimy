using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportAcademy.Application.Commands.BranchCommands.AddSportToBranch;
using SportAcademy.Application.Commands.BranchCommands.CreateBranch;
using SportAcademy.Application.Commands.BranchCommands.DeactivateBranch;
using SportAcademy.Application.Commands.BranchCommands.DeleteBranch;
using SportAcademy.Application.Commands.BranchCommands.UpdateBranch;
using SportAcademy.Application.Common.Pagination;
using SportAcademy.Application.Queries.BranchQueries;
using SportAcademy.Application.Queries.BranchQueries.GetAll;
using SportAcademy.Application.Queries.BranchQueries.GetBranchStats;
using SportAcademy.Application.Queries.BranchQueries.GetBranchesCount;
using SportAcademy.Application.Queries.BranchQueries.GetById;
using SportAcademy.Application.Queries.BranchQueries.GetDropdown;
using SportAcademy.Application.Queries.BranchQueries.SearchBranch;


namespace SportAcademy.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
	[ApiController]
	public class BranchController : ControllerBase
	{
		private readonly IMediator _mediator;

		public BranchController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] CreateBranchCommand command, CancellationToken cancellationToken)
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
				new GetAllBranchesQuery(PageRequest.Create(page, pageSize)), ct);
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			var result = await _mediator.Send(new GetBranchByIdQuery(id));
			return Ok(result);
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Update(int id, [FromBody] UpdateBranchCommand command,
			CancellationToken cancellationToken)
		{
			command = command with { Id = id };
			var result = await _mediator.Send(command, cancellationToken);
			return Ok(result);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _mediator.Send(new DeleteBranchCommand(id));
			return Ok(result);
		}

        [HttpPost("branch-sports")]
        public async Task<IActionResult> AddSportToBranch([FromBody] AddSportToBranchCommand command,
			CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            return Ok(result);
        }

		[HttpGet("count")]
		public async Task<IActionResult> GetBranchesCount(CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new GetBranchesCountQuery(), cancellationToken);
			return Ok(result);
        }

        [HttpGet("{id}/capacity")]
        public async Task<IActionResult> GetBranchCapacity(int id, CancellationToken ct)
        {
            var result = await _mediator.Send(
                new GetBranchTotalCapacityQuery(id), ct);

            return Ok(result);
        }

        [HttpGet("dropdown")]
        public async Task<IActionResult> GetDropdown(CancellationToken ct)
        {
            var result = await _mediator.Send(new GetBranchesDropdownQuery(), ct);
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
                new SearchBranchQuery(searchTerm, PageRequest.Create(page, pageSize)), ct);
            return Ok(result);
        }

        [HttpGet("{id}/stats")]
        public async Task<IActionResult> GetStats(int id, CancellationToken ct)
        {
            var result = await _mediator.Send(new GetBranchStatsQuery(id), ct);
            return Ok(result);
        }

        [HttpPatch("{id}/deactivate")]
        public async Task<IActionResult> Deactivate(int id, CancellationToken ct)
        {
            var result = await _mediator.Send(new DeactivateBranchCommand(id), ct);
            return Ok(result);
        }
    }
}

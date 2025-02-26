using Application.Features.ApplicationRole.Commands.CreateRole;
using Application.Features.ApplicationRole.Commands.UpdateRole;
using Application.Features.ApplicationRole.Queries.GetAllRoles;
using Application.Features.ApplicationRole.Queries.GetRoleDetail;
using Application.Features.ApplicationUser.Commands.DeleteUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IMediator _mediator;

    public RolesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create-role")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> CreateRole(CreateRoleCommand request)
    {
        var result = await _mediator.Send(request);
        return Ok(result);
    }

    [HttpGet("get-roles")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [EnableRateLimiting(policyName: "fixed")]
    public async Task<ActionResult> GetUsers()
    {
        var roles = await _mediator.Send(new GetRolesQuery());
        return Ok(roles);
    }

    [HttpGet("get-role/{id}")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [EnableRateLimiting(policyName: "fixed")]
    public async Task<ActionResult> GetRole(Guid id)
    {
        var role = await _mediator.Send(new GetRoleDetailQuery(id));
        return Ok(role);
    }

    [HttpPut("update-role/{id}")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> UpdateRole(UpdateRoleCommand request)
    {
        var role = await _mediator.Send(request);
        return Ok(role);
    }

    [HttpDelete("delete-role/{id}")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> DeleteRole(Guid id)
    {
        var result = await _mediator.Send(new RemoveUserRoleCommand(id));
        return Ok(result);
    }
}

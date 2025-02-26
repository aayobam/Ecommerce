using Application.Contracts.Persistence;
using Application.DTOs.Auth;
using Application.Filters;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthsController : ControllerBase
{
    private readonly IAuthRepository _authRepository;

    public AuthsController(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    [HttpPost("user-login")]
    [ValidationFilter<UserLoginVm>]
    [SwaggerOperation(Summary = "user login")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Login([FromBody] UserLoginVm request)
    {
        var response = await _authRepository.LoginAsync(request);
        return Ok(response);
    }

    [HttpPost("user-logout")]
    [SwaggerOperation(Summary = "user logout")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Logout()
    {
        var response = await _authRepository.LogoutAsync();
        return Ok(response);
    }

    [HttpPost("forgot-password")]
    [ValidationFilter<ForgotPasswordVm>]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "forgot password")]
    public async Task<ActionResult> ForgotPassword([FromBody] ForgotPasswordVm request)
    {
        var response = await _authRepository.ForgotPasswordAsync(request);
        return Ok(response);
    }

    [HttpPost("set-password")]
    [ValidationFilter<SetPasswordVm>]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "Set new password")]
    public async Task<ActionResult> SetPassword([FromBody] SetPasswordVm request)
    {
        var response = await _authRepository.SetPasswordAsync(request);
        return Ok(response);
    }
}

using Application.Contracts.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AmazonSecretManagerController : ControllerBase
{
    private readonly IAwsRepository _awsRepository;

    public AmazonSecretManagerController(IAwsRepository awsRepository)
    {
        _awsRepository = awsRepository;
    }

    [HttpGet("get-appsettings-config")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [SwaggerOperation(Summary = "gets app settings configuration from cloud secret managers, e,g amazon secret manager or azure vaults")]
    public async Task<IActionResult> GetAmazonSecret()
    {
        var result = await _awsRepository.GetAppSettingsConfigAsync();
        return Ok(result);
    }
}

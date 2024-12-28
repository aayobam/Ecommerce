using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VendorController : ControllerBase
{
    private readonly IMediator _mediator;

    public VendorController(IMediator mediator)
    {
        _mediator = mediator;
    }
}

using Application.Features.Product.Commands.CreateProduct;
using Application.Features.Product.Commands.UpdateProduct;
using Application.Features.Product.Queries.GetAllProducts;
using Application.Features.Product.Queries.GetProductDetails;
using Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create-product")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<GenericResponse> CreateProduct(CreateProductCommand request)
    {
        var result = await _mediator.Send(request);

        return new GenericResponse()
        {
            Success = true,
            Message = "success",
            Data = result,
            Status = HttpStatusCode.Created.ToString()
        };
    }

    [HttpGet("get-products")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<GenericResponse> GetProducts()
    {
        var products = await _mediator.Send(new GetAllProductsQuery());

        return new GenericResponse()
        {
            Success = true,
            Message= "success",
            Data = products,
            Status = HttpStatusCode.OK.ToString()
        };
    }

    [HttpGet("get-product/{id}")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<GenericResponse> GetProduct(Guid id)
    {
        var product = await _mediator.Send(new GetProductDetailsQuery(id));

        return new GenericResponse()
        {
            Success = true,
            Message = "success",
            Data = product,
            Status = HttpStatusCode.OK.ToString()
        };
    }

    [HttpGet("update-product/{id}")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<GenericResponse> UpdateProduct(UpdateProductCommand request)
    {
        var product = await _mediator.Send(request);

        return new GenericResponse()
        {
            Success = true,
            Message = "success",
            Data = product,
            Status = HttpStatusCode.OK.ToString()
        };
    }

    [HttpGet("delete-product/{id}")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<GenericResponse> DeleteProduct(UpdateProductCommand request)
    {
        var product = await _mediator.Send(request);

        return new GenericResponse()
        {
            Success = true,
            Message = "success",
            Data = product,
            Status = HttpStatusCode.OK.ToString()
        };
    }
}

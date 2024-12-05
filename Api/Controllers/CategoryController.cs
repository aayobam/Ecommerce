using Application.DTOs.Category;
using Application.Features.Category.Commands.CreateCategory;
using Application.Features.Category.Commands.DeleteCategory;
using Application.Features.Category.Commands.UpdateCategory;
using Application.Features.Category.Queries.GetAllCategory;
using Application.Features.Category.Queries.GetCatetoryQuery;
using Application.Responses;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("get-categories")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GenericResponse> GetCategories()
    {
        var categories = await _mediator.Send(new GetCategoriesQuery());

        return new GenericResponse()
        {
            Success = true,
            Message = "success",
            Data = categories,
            Status = HttpStatusCode.OK.ToString()
        };
    }

    [HttpGet("get-category/{id}")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryVm>> GetCategory(Guid id)
    {
        var category = await _mediator.Send(new GetCategoryDetailsQuery(id));
        return category;
    }

    [HttpPost("create-category")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<GenericResponse> CreateCategory(CreateCategoryCommand request)
    {
        var category = await _mediator.Send(request);

        return new GenericResponse()
        {   Success = true,
            Message = "success",
            Data = category,
            Status = HttpStatusCode.Created.ToString()
        };
    }

    [HttpPut("update-category/{id}")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<GenericResponse> UpdateCategory(UpdateCategoryCommand request)
    {
        var category = await _mediator.Send(request);

        return new GenericResponse()
        {
            Success = true,
            Message = "success",
            Data = category,
            Status = HttpStatusCode.Created.ToString()
        };
    }

    [HttpDelete("delete-category/{id}")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<GenericResponse> DeleteCategory(DeleteCategoryCommand request)
    {
        var category = await _mediator.Send(request);

        return new GenericResponse()
        {
            Success = true,
            Message = "success",
            Data = category,
            Status = HttpStatusCode.Created.ToString()
        };
    }
}

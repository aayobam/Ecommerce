using Application.Contracts.Persistence;
using Application.DTOs.Category;
using Application.Features.Category.Commands.CreateCategory;
using Application.Features.Category.Commands.UpdateCategory;
using Application.Features.Category.Queries.GetAllCategory;
using Application.Features.Category.Queries.GetCatetoryQuery;
using Application.Responses;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CategoryController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<GenericResponse> GetAllCategory()
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

    [HttpGet("{id}")]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CategoryVm>> GetCategory(Guid id)
    {
        var category = await _mediator.Send(new GetCategoryDetailsQuery(id));
        return category;
    }

    [HttpPost]
    [ProducesDefaultResponseType]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

    [HttpPut("{id}")]
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

    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}

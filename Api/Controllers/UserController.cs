using Application.Features.ApplicationUser.Commands.CreateUser;
using Application.Features.ApplicationUser.Commands.DeleteUser;
using Application.Features.ApplicationUser.Commands.UpdateUser;
using Application.Features.ApplicationUser.Queries.GetAllUser;
using Application.Features.ApplicationUser.Queries.GetUserDetail;
using Application.Responses;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("create-user")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<GenericResponse> CreateUser(CreateUserCommand request)
        {
            var result = await _mediator.Send(request);
            var response = new GenericResponse()
            {
                Message = "success",
                Success = true,
                Data = result,
                Status = HttpStatusCode.Created.ToString()
            };
            return response;
        }

        [HttpGet("get-users")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<GenericResponse> GetUsers()
        {
            var users = await _mediator.Send(new GetAllUserQuery());
            var response = new GenericResponse()
            {
                Message = "success",
                Success = true,
                Data = users,
                Status = HttpStatusCode.Created.ToString()
            };
            return response;
        }

        [HttpGet("get-user/{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<GenericResponse> GetUser(Guid id)
        {
            var user = await _mediator.Send(new GetUserDetailQuery(id));
            var response = new GenericResponse()
            {
                Message = "success",
                Success = true,
                Data = user,
                Status = HttpStatusCode.OK.ToString()
            };
            return response;
        }

        [HttpPut("update-user/{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<GenericResponse> UpdateUser(UpdateUserCommand request)
        {
            var user = await _mediator.Send(request);

            var response = new GenericResponse()
            {
                Success = true,
                Message = "success",
                Data = user,
                Status = HttpStatusCode.OK.ToString()
            };
            return response;
        }

        [HttpDelete("delete-user/{id}")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<GenericResponse> DeleteUser(Guid id)
        {
            var user = await _mediator.Send(new DeleteUserCommand(id));
            var response = new GenericResponse()
            {
                Message = "success",
                Success = true,
                Data = user,
                Status = HttpStatusCode.NoContent.ToString()
            };
            return response;
        }
    }
}

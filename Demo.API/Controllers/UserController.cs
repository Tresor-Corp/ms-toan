using Demo.Application.Command;
using Demo.Application.Query;
using Demo.Core.Entity;
using Demo.Core.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Demo.API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<BaseResponse<TokenResponse>> Login(LoginQuery request)
        {
            return await _mediator.Send(request);
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<BaseResponse<User>> Register(CreateUserCommand request)
        {
            return await _mediator.Send(request);
        }
        [HttpGet("profile")]
        [Authorize]
        public async Task<BaseResponse<User>> GetUserProfile()
        {
            var id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            return await _mediator.Send(new GetUserProfileQuery(Guid.Parse(id)));
        }
    }
}

using System.Linq;
using System.Security.Claims;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Extensions;
using RestWebservice_RemoteCompiling.Helpers;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Controllers
{
    [Route("api/user")]
    [ApiController]
    [EnableCors("AllAllowedPolicy")]
    [Authorize]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly UserRepository _userRepository;
        public UserController(IMediator mediator, RemoteCompileDbContext context, ITokenService tokenService)
            : base(tokenService)
        {
            _mediator = mediator;
            _userRepository = new UserRepository(context);
        }

        [HttpGet("getUser")]
        public IActionResult GetUser(string ldapIdent)
        {
            return CustomResponse.Success(_userRepository.GetUserByLdapUid(ldapIdent)).ToResponse();
        }
        
        [HttpGet("getMySelf")]
        public IActionResult GetMyself()
        {
            var token = GetTokenFromAuthorization();
            return CustomResponse.Success(_userRepository.GetUserByLdapUid(token.Claims.First(x => x.Type == ClaimTypes.Sid).Value)).ToResponse();
        }
        
    }
}
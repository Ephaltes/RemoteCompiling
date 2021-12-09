using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public abstract class BaseHandler<TRequest, TResponse>  : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IUserRepository _userRepository;
        public BaseHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        protected User GetUserFromToken(JwtSecurityToken token)
        {
            string ldapIdent = token.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            User? user = _userRepository.GetUserByLdapUid(ldapIdent);

            if (user is null)
                throw new AccessViolationException("No User found");

            return user;
        }

        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
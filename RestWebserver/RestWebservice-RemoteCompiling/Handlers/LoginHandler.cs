using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Helpers;
using RestWebservice_RemoteCompiling.Repositories;
using Serilog;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public class LoginHandler : IRequestHandler<LoginCommand, CustomResponse<SessionUser>>
    {
        private readonly ILdapHelper _ldapHelper;
        private readonly IUserRepository _userRepository;
        private readonly ISessionRepository _sessionRepository;
        public LoginHandler(ILdapHelper ldapHelper, IUserRepository userRepository, ISessionRepository sessionRepository)
        {
            _ldapHelper = ldapHelper;
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
        }

        public async Task<CustomResponse<SessionUser>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                LdapUser? user = _ldapHelper.LogInUser(request.Username, request.Password);

                if (user is null)
                    return CustomResponse.Error<SessionUser>(401, "Invalid Credentials");

                User? dbUser = _userRepository.GetUserByLdapUid(user.Uid);

                if (dbUser is null)
                {
                    dbUser = new User
                    {
                        Email = user.Mail,
                        Name = user.Cn,
                        LdapUid = user.Uid,
                        UserRole = UserRole.DefaultUser
                    };

                    dbUser = _userRepository.AddUser(dbUser);
                }

                Session session = new Session() { LdapUser = dbUser };
                session = _sessionRepository.Add(session);

                SessionUser sessionUser = new SessionUser() { Token = session.Id.ToString("N"), User = user };

                return CustomResponse.Success(sessionUser);
            }
            catch (Exception e)
            {
                Log.Error(e, $"{e.Message} \n\n{e.StackTrace}");

                return CustomResponse.Error<SessionUser>(500, "Unexpected Error");
            }
        }
    }
}
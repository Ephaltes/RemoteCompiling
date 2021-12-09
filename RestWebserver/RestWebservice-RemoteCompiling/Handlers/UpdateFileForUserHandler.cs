using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public class UpdateFileForUserHandler : IRequestHandler<UpdateFileForProjectCommand, CustomResponse<bool>>
    {
        private readonly IUserRepository _userRepository;
        public UpdateFileForUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CustomResponse<bool>> Handle(UpdateFileForProjectCommand request, CancellationToken cancellationToken)
        {
            string ldapIdent = request.Token.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            User? ldapUser = _userRepository.GetUserByLdapUid(ldapIdent);


            foreach (Project item in ldapUser.Projects.Where(x => x.Id == request.ProjectId))
            {
                foreach (File file in item.Files.Where(file => file.Id == request.FileId))
                {
                    file.FileName = request.FileName;

                    break;
                }
            }

            _userRepository.UpdateUser(ldapUser);

            return CustomResponse.Success(true);
        }
    }
}
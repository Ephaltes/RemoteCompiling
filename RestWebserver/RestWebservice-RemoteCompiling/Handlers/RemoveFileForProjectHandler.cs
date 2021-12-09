using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public class RemoveFileForProjectHandler : BaseHandler<RemoveFileForProjectCommand, CustomResponse<bool>>
    {
        private readonly IUserRepository _userRepository;
        public RemoveFileForProjectHandler(IUserRepository userRepository)
            : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public override async Task<CustomResponse<bool>> Handle(RemoveFileForProjectCommand request, CancellationToken cancellationToken)
        {
            string ldapIdent = request.Token.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            User? ldapUser = _userRepository.GetUserByLdapUid(ldapIdent);

            Project projectInWhichToDelete = ldapUser.Projects.FirstOrDefault(x => x.Id == request.ProjectId);
            File? fileToDelete = projectInWhichToDelete.Files.FirstOrDefault(x => x.Id == request.FileId);

            if (fileToDelete is not null)
            {
                foreach (Project item in ldapUser.Projects)
                {
                    if (item.Id == request.ProjectId)
                    {
                        item.Files.Remove(fileToDelete);

                        break;
                    }
                }

                _userRepository.UpdateUser(ldapUser);
            }

            return CustomResponse.Success(true);
        }
    }
}
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
    public class RemoveFileForProjectHandler : IRequestHandler<RemoveFileForProjectCommand, CustomResponse<bool>>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IUserRepository _userRepository;
        public RemoveFileForProjectHandler(IUserRepository userRepository, IExerciseRepository exerciseRepository)
        {
            _userRepository = userRepository;
            _exerciseRepository = exerciseRepository;
        }

        public async Task<CustomResponse<bool>> Handle(RemoveFileForProjectCommand request, CancellationToken cancellationToken)
        {
            string ldapIdent = request.Token.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            User? ldapUser = _userRepository.GetUserByLdapUid(ldapIdent);

            Project projectInWhichToDelete = ldapUser.Projects.FirstOrDefault(x => x.Id == request.ProjectId);
            var fileToDelete = projectInWhichToDelete.Files.FirstOrDefault(x => x.Id == request.FileId);
                
            if (fileToDelete is not null)
            {
                foreach (var item in ldapUser.Projects)
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
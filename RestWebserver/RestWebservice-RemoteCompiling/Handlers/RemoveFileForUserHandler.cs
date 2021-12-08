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
    public class RemoveFileForUserHandler : IRequestHandler<RemoveFileForUserCommand, CustomResponse<bool>>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IUserRepository _userRepository;
        public RemoveFileForUserHandler(IUserRepository userRepository, IExerciseRepository exerciseRepository)
        {
            _userRepository = userRepository;
            _exerciseRepository = exerciseRepository;
        }

        public async Task<CustomResponse<bool>> Handle(RemoveFileForUserCommand request, CancellationToken cancellationToken)
        {
            string ldapIdent = request.Token.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            User? ldapUser = _userRepository.GetUserByLdapUid(ldapIdent);

            File? fileToDelete = ldapUser.Files.FirstOrDefault(x => x.Id == request.FileId);

            if (fileToDelete is not null)
            {
                ldapUser.Files.Remove(fileToDelete);
                _userRepository.UpdateUser(ldapUser);
            }

            return CustomResponse.Success(true);
        }
    }
}
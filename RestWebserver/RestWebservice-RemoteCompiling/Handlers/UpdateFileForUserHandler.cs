using System;
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
    public class UpdateFileForUserHandler : IRequestHandler<UpdateFileForUserCommand, CustomResponse<bool>>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IUserRepository _userRepository;
        public UpdateFileForUserHandler(IUserRepository userRepository, IExerciseRepository exerciseRepository)
        {
            _userRepository = userRepository;
            _exerciseRepository = exerciseRepository;
        }

        public async Task<CustomResponse<bool>> Handle(UpdateFileForUserCommand request, CancellationToken cancellationToken)
        {
            string ldapIdent = request.Token.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            User? ldapUser = _userRepository.GetUserByLdapUid(ldapIdent);

            var file = ldapUser.Files.FirstOrDefault(x => x.Id == request.FileId);
            
            if (file is null)
            {
                return CustomResponse.Error<bool>(404, "File not found");
            }

            file.FileName = request.FileName;
            
            _userRepository.UpdateUser(ldapUser);
            
            return CustomResponse.Success(true);
        }
    }
}
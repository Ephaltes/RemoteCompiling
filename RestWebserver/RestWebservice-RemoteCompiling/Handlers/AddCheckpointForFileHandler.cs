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
    public class AddCheckpointForFileHandler : BaseHandler<AddCheckpointForFileCommand, CustomResponse<int>>
    {
        private readonly IFileRepository _fileRepository;
        private readonly IUserRepository _userRepository;
        public AddCheckpointForFileHandler(IUserRepository userRepository, IFileRepository fileRepository)
            : base(userRepository)
        {
            _userRepository = userRepository;
            _fileRepository = fileRepository;
        }
        public override async Task<CustomResponse<int>> Handle(AddCheckpointForFileCommand request, CancellationToken cancellationToken)
        {
            string ldapIdent = request.Token.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            User? ldapUser = await _userRepository.GetUserByLdapUid(ldapIdent);

            if (ldapUser is null)
            {
                return CustomResponse.Error<int>(403);
            }

            bool isOwner = await _fileRepository.UserIsOwnerOfFile(ldapUser.LdapUid, request.FileId);

            if (!isOwner)
            {
                return CustomResponse.Error<int>(403);
            }

            File? file = await _fileRepository.GetFile(request.FileId);

            if (file is null)
            {
                return CustomResponse.Error<int>(404, "File not found");
            }

            file.Checkpoints.Add(request.Checkpoint);

            await _fileRepository.Update(file);

            return CustomResponse.Success(request.Checkpoint.Id);
        }
    }
}
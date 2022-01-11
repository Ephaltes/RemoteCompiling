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
        private readonly IFileRepository _fileRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        public RemoveFileForProjectHandler(IUserRepository userRepository, IFileRepository fileRepository, IProjectRepository projectRepository)
            : base(userRepository)
        {
            _userRepository = userRepository;
            _fileRepository = fileRepository;
            _projectRepository = projectRepository;
        }

        public override async Task<CustomResponse<bool>> Handle(RemoveFileForProjectCommand request, CancellationToken cancellationToken)
        {
            string ldapIdent = request.Token.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            User? ldapUser = await _userRepository.GetUserByLdapUid(ldapIdent);

            if (ldapUser is null)
            {
                return CustomResponse.Error<bool>(403);
            }

            Project? project = await _projectRepository.GetProjectIfUserHasAccess(request.ProjectId, ldapUser.LdapUid);

            if (project is null)
            {
                return CustomResponse.Error<bool>(403, "Project not found or no access");
            }

            bool isOwner = await _fileRepository.UserIsOwnerOfFile(ldapUser.LdapUid, request.FileId);

            if (!isOwner)
            {
                return CustomResponse.Error<bool>(403, "File not found or no access");
            }

            File file = await _fileRepository.GetFile(request.FileId);

            project.Files.Remove(file);
            await _projectRepository.Update(project);

            return CustomResponse.Success(true);
        }
    }
}
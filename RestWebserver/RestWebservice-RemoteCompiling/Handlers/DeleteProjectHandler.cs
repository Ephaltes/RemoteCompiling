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
    public class DeleteProjectHandler : BaseHandler<DeleteProjectCommand, CustomResponse<bool>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public DeleteProjectHandler(IUserRepository userRepository, IProjectRepository projectRepository)
            : base(userRepository)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
        }

        public override async Task<CustomResponse<bool>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            string ldapIdent = request.Token.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            User? ldapUser = await _userRepository.GetUserByLdapUid(ldapIdent);

            if (ldapUser is null)
            {
                return CustomResponse.Error<bool>(403);
            }

            Project? project = ldapUser.Projects.FirstOrDefault(x => x.Id == request.ProjectId);

            if (project is null)
            {
                return CustomResponse.Error<bool>(403);
            }

            ldapUser.Projects.Remove(project);

            await _userRepository.UpdateUser(ldapUser);

            return CustomResponse.Success(true);
        }
    }
}
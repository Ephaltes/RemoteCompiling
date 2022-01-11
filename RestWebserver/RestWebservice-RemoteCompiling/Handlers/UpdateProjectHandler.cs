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
    public class UpdateProjectHandler : BaseHandler<UpdateProjectCommand, CustomResponse<bool>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;

        public UpdateProjectHandler(IUserRepository userRepository, IProjectRepository projectRepository)
            : base(userRepository)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
        }
        public override async Task<CustomResponse<bool>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
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

            if (request.ProjectName is not null)
                project.ProjectName = request.ProjectName;

            if (request.ProjectType is not null)
                project.ProjectType = request.ProjectType.Value;

            if (request.StdIn is not null)
                project.StdIn = request.StdIn;

            await _projectRepository.Update(project);

            return CustomResponse.Success(true);
        }
    }
}
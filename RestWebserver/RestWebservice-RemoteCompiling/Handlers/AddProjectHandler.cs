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
    public class AddProjectHandler : BaseHandler<AddProjectCommand, CustomResponse<int>>
    {
        private readonly IUserRepository _userRepository;
        public AddProjectHandler(IUserRepository userRepository)
            : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public override async Task<CustomResponse<int>> Handle(AddProjectCommand request, CancellationToken cancellationToken)
        {
            string ldapIdent = request.Token.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            User? ldapUser = _userRepository.GetUserByLdapUid(ldapIdent);

            Project project = new Project
                              {
                                  ProjectName = request.Project.ProjectName,
                                  StdIn = request.Project.StdIn
                              };

            ldapUser.Projects.Add(project);

            _userRepository.UpdateUser(ldapUser);

            return CustomResponse.Success(project.Id);
        }
    }
}
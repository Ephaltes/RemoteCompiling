using System.Collections.Generic;
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
    public class AddFileForProjectHandler : BaseHandler<AddFileForProjectCommand, CustomResponse<int>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IUserRepository _userRepository;
        public AddFileForProjectHandler(IUserRepository userRepository, IProjectRepository projectRepository)
            : base(userRepository)
        {
            _userRepository = userRepository;
            _projectRepository = projectRepository;
        }

        public override async Task<CustomResponse<int>> Handle(AddFileForProjectCommand request, CancellationToken cancellationToken)
        {
            string ldapIdent = request.Token.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            User? ldapUser = await _userRepository.GetUserByLdapUid(ldapIdent);

            if (ldapUser == null)
            {
                return CustomResponse.Error<int>(403);
            }

            Project? project = await _projectRepository.GetProjectIfUserHasAccess(request.ProjectId, ldapUser.LdapUid);

            if (project is null)
            {
                return CustomResponse.Error<int>(403);
            }

            File file = new File
                        {
                            Checkpoints = new List<Checkpoint>
                                          {
                                              new Checkpoint
                                              {
                                                  Code = request.File.Checkpoints.Last().Code
                                              }
                                          },
                            FileName = request.File.FileName
                        };

            project.Files.Add(file);
            await _projectRepository.Update(project);

            return CustomResponse.Success(file.Id);
        }
    }
}
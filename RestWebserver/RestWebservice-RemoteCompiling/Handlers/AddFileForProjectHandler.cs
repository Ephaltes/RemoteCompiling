using System.Collections.Generic;
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
    public class AddFileForProjectHandler : IRequestHandler<AddFileForProjectCommand, CustomResponse<int>>
    {
        private readonly IUserRepository _userRepository;
        public AddFileForProjectHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CustomResponse<int>> Handle(AddFileForProjectCommand request, CancellationToken cancellationToken)
        {
            string ldapIdent = request.Token.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            User? ldapUser = _userRepository.GetUserByLdapUid(ldapIdent);

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

            User? user = _userRepository.GetUserByLdapUid(ldapUser.LdapUid);
            foreach (Project x in user.Projects)
            {
                if (x.Id == request.ProjectId)
                {
                    x.Files.Add(file);

                    break;
                }
            }

            _userRepository.UpdateUser(user);

            return CustomResponse.Success(file.Id);
        }
    }
}
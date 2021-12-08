using System;
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
    public class AddFileForUserHandler : IRequestHandler<AddFileForUserCommand,CustomResponse<bool>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IExerciseRepository _exerciseRepository;
        public AddFileForUserHandler(IUserRepository userRepository, IExerciseRepository exerciseRepository)
        {
            _userRepository = userRepository;
            _exerciseRepository = exerciseRepository;
        }

        public async Task<CustomResponse<bool>> Handle(AddFileForUserCommand request, CancellationToken cancellationToken)
        {
            string ldapIdent = request.Token.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            var ldapUser = _userRepository.GetUserByLdapUid(ldapIdent);

            File file = new File()
                        {
                            Checkpoints = new List<Checkpoint>()
                                          {
                                              new Checkpoint()
                                              {
                                                  Code = request.File.CodeEntity.Value,
                                              }
                                          },
                            Exercise = null,
                            FileName = request.File.Name,
                            FileNodeType = request.File.Type
                        };

            var user = _userRepository.GetUserByLdapUid(ldapUser.LdapUid);
            user.Files.Add(file);
            
            _userRepository.UpdateUser(user);

            return CustomResponse.Success(true);
        }
    }
}
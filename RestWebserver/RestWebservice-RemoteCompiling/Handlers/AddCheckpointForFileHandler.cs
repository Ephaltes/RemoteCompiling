using System;
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
        private readonly IUserRepository _userRepository;
        public AddCheckpointForFileHandler(IUserRepository userRepository)
            : base(userRepository)
        {
            _userRepository = userRepository;
        }
        public override async Task<CustomResponse<int>> Handle(AddCheckpointForFileCommand request, CancellationToken cancellationToken)
        {
            string ldapIdent = request.Token.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            User? ldapUser = await _userRepository.GetUserByLdapUid(ldapIdent);
            

            bool flag = false;
            foreach (Project ldapUserProject in ldapUser.Projects)
            {
                ldapUserProject.Files.ForEach(x =>
                                              {
                                                  if (x.Id == request.FileId)
                                                  {
                                                      x.LastModified = DateTime.Now;
                                                      x.Checkpoints.Add(request.Checkpoint);
                                                      flag = true;
                                                      return;
                                                  }
                                              });
            }

            if (!flag)
            {
                return CustomResponse.Error<int>(404, "File not found");
            }
            _userRepository.UpdateUser(ldapUser);

            return CustomResponse.Success(request.Checkpoint.Id);
        }
    }
}
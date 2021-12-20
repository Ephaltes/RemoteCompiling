using System.Linq;
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
        private readonly IUserRepository _userRepository;

        public DeleteProjectHandler(IUserRepository userRepository)
            : base(userRepository)
        {
            _userRepository = userRepository;
        }

        public override async Task<CustomResponse<bool>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            User user = GetUserFromToken(request.Token);

            Project? project = user.Projects.FirstOrDefault(x => x.Id == request.ProjectId);

            user.Projects.Remove(project);

            _userRepository.UpdateUser(user);

            return CustomResponse.Success(true);
        }
    }
}
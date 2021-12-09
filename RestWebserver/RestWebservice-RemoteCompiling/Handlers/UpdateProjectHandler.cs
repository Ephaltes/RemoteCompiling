using System.Linq;
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
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IUserRepository _userRepository;

        public UpdateProjectHandler(IUserRepository userRepository, IExerciseRepository exerciseRepository)
            : base(userRepository)
        {
            _userRepository = userRepository;
            _exerciseRepository = exerciseRepository;
        }
        public override async Task<CustomResponse<bool>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            User user = GetUserFromToken(request.Token);

            Project? project = user.Projects.FirstOrDefault(x => x.Id == request.ProjectId);

            if (request.ProjectName is not null)
                project.ProjectName = request.ProjectName;

            if (request.ProjectType is not null)
                project.ProjectType = request.ProjectType.Value;

            if (request.StdIn is not null)
                project.stdin = request.StdIn;

            _userRepository.UpdateUser(user);

            return CustomResponse.Success(true);
        }
    }
}
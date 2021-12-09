using System.Threading;
using System.Threading.Tasks;

using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public class DeleteExerciseHandler : BaseHandler<DeleteExerciseCommand, CustomResponse<bool>>
    {
        private readonly IExerciseRepository _exerciseRepository;

        public DeleteExerciseHandler(IExerciseRepository exerciseRepository, IUserRepository userRepository)
            : base(userRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        public override async Task<CustomResponse<bool>> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
        {
            _exerciseRepository.Delete(request.Id);

            return CustomResponse.Success(true);
        }
    }
}
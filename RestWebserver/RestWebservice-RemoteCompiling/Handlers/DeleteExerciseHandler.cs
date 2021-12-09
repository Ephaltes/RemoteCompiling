using System.Threading;
using System.Threading.Tasks;

using MediatR;

using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public class DeleteExerciseHandler : IRequestHandler<DeleteExerciseCommand, CustomResponse<bool>>
    {
        private readonly IExerciseRepository _exerciseRepository;

        public DeleteExerciseHandler( IExerciseRepository exerciseRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        public async Task<CustomResponse<bool>> Handle(DeleteExerciseCommand request, CancellationToken cancellationToken)
        {
            _exerciseRepository.Delete(request.Id);

            return CustomResponse.Success(true);
        }
    }
}
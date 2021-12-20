using System.Threading;
using System.Threading.Tasks;

using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Query;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public class GetExerciseHandler : BaseHandler<GetExerciseQuery, CustomResponse<ExerciseEntity>>
    {
        private readonly IExerciseRepository _exerciseRepository;
        public GetExerciseHandler(IExerciseRepository exerciseRepository, IUserRepository userRepository)
            : base(userRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        public override async Task<CustomResponse<ExerciseEntity>> Handle(GetExerciseQuery request, CancellationToken cancellationToken)
        {
            Exercise? dbExercise = _exerciseRepository.Get(request.Id);

            if (dbExercise is null)
                return CustomResponse.Error<ExerciseEntity>(404, "Exercise not found");

            ExerciseEntity exerciseEntity = new ExerciseEntity
                                            {
                                                Author = dbExercise.Author.Name,
                                                Description = dbExercise.Description,
                                                Id = dbExercise.Id,
                                                Name = dbExercise.Name,
                                                Template = dbExercise.Template,
                                                HandIns = dbExercise.HandIns,
                                                DueDate = dbExercise.DueDate,
                                                TaskDefinition = dbExercise.TaskDefinition
                                            };


            return CustomResponse.Success(exerciseEntity);
        }
    }
}
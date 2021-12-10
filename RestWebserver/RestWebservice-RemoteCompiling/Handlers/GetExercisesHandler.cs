using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Query;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public class GetExercisesHandler : BaseHandler<GetExercisesQuery, CustomResponse<List<ExerciseEntity>>>
    {
        private readonly IExerciseRepository _exerciseRepository;
        public GetExercisesHandler(IExerciseRepository exerciseRepository, IUserRepository userRepository)
            : base(userRepository)
        {
            _exerciseRepository = exerciseRepository;
        }

        public override async Task<CustomResponse<List<ExerciseEntity>>> Handle(GetExercisesQuery request, CancellationToken cancellationToken)
        {
            List<Exercise> dbExerciseList = _exerciseRepository.GetAll();
            List<ExerciseEntity> exerciseList = new List<ExerciseEntity>();

            if (dbExerciseList is null || dbExerciseList.Count == 0)
                return CustomResponse.Success(exerciseList);

            foreach (Exercise item in dbExerciseList)
            {
                ExerciseEntity x = new ExerciseEntity
                                   {
                                       Author = item.Author.Name,
                                       Description = item.Description,
                                       Id = item.Id,
                                       Name = item.Name,
                                       Template = item.Template,
                                       HandIns = item.HandIns,
                                       DueDate = item.DueDate,
                                       TaskDefinition = item.TaskDefinition
                                   };
                exerciseList.Add(x);
            }

            return CustomResponse.Success(exerciseList);
        }
    }
}
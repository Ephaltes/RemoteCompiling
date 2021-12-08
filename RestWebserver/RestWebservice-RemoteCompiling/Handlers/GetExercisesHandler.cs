using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Query;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public class GetExercisesHandler : IRequestHandler<GetExercisesQuery, CustomResponse<List<ExerciseEntity>>>
    {
        private readonly IExerciseGradeRepository _exerciseGradeRepository;
        private readonly IExerciseRepository _exerciseRepository;
        public GetExercisesHandler(IExerciseRepository exerciseRepository, IExerciseGradeRepository exerciseGradeRepository)
        {
            _exerciseRepository = exerciseRepository;
            _exerciseGradeRepository = exerciseGradeRepository;
        }
        public async Task<CustomResponse<List<ExerciseEntity>>> Handle(GetExercisesQuery request, CancellationToken cancellationToken)
        {
            List<Database.Exercise> dbExerciseList = _exerciseRepository.GetAll();
            List<ExerciseEntity> exerciseList = new List<ExerciseEntity>();

            if (dbExerciseList is null || dbExerciseList.Count == 0)
                return CustomResponse.Success(exerciseList);

            foreach (var item in dbExerciseList)
            {
                var x = new ExerciseEntity()
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
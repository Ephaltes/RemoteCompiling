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
    public class GetExerciseHandler : IRequestHandler<GetExerciseQuery, CustomResponse<ExerciseEntity>>
    {
        private readonly IExerciseGradeRepository _exerciseGradeRepository;
        private readonly IExerciseRepository _exerciseRepository;
        public GetExerciseHandler(IExerciseRepository exerciseRepository, IExerciseGradeRepository exerciseGradeRepository)
        {
            _exerciseRepository = exerciseRepository;
            _exerciseGradeRepository = exerciseGradeRepository;
        }
        public async Task<CustomResponse<ExerciseEntity>> Handle(GetExerciseQuery request, CancellationToken cancellationToken)
        {
            Database.Exercise? dbExercise = _exerciseRepository.Get(request.Id);

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
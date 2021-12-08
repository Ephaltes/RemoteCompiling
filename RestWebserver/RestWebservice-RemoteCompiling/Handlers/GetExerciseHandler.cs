using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Query;
using RestWebservice_RemoteCompiling.Repositories;

using Exercise = RestWebservice_RemoteCompiling.Entities.Exercise;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public class GetExerciseHandler : IRequestHandler<GetExerciseQuery, CustomResponse<Exercise>>
    {
        private readonly IExerciseGradeRepository _exerciseGradeRepository;
        private readonly IExerciseRepository _exerciseRepository;
        public GetExerciseHandler(IExerciseRepository exerciseRepository, IExerciseGradeRepository exerciseGradeRepository)
        {
            _exerciseRepository = exerciseRepository;
            _exerciseGradeRepository = exerciseGradeRepository;
        }
        public async Task<CustomResponse<Exercise>> Handle(GetExerciseQuery request, CancellationToken cancellationToken)
        {
            Database.Exercise? dbExercise = _exerciseRepository.Get(request.Id);

            if (dbExercise is null)
                return CustomResponse.Error<Exercise>(404, "Exercise not found");

            List<Students> studentsList = new List<Students>();

            List<Files> filesList = dbExercise.Files.Where(x => x.User is null)
                .Select(file => new Files
                                {
                                    Name = file.FileName,
                                    Type = file.FileNodeType,
                                    CodeEntity = new CodeEntity
                                                 {
                                                     Language = file.FileNodeType.ToString(),
                                                     Uri = file.FileName,
                                                     Value = file.Checkpoint.Code
                                                 }
                                })
                .ToList();

            foreach (ExerciseGrade dbExerciseGrade in _exerciseGradeRepository.Get(dbExercise.Id))
            {
                if (studentsList.Select(x => x.Id).Contains(dbExerciseGrade.UserToGrade.LdapUid))
                    continue;

                IEnumerable<ExerciseFile> studentFiles = dbExercise.Files.Where(x => x.User?.LdapUid == dbExerciseGrade.UserToGrade.LdapUid);

                List<Files> studentFilesList = studentFiles.Select(file => new Files
                                                                           {
                                                                               Name = file.FileName,
                                                                               Type = file.FileNodeType,
                                                                               CodeEntity = new CodeEntity
                                                                                            {
                                                                                                Language = file.FileNodeType.ToString(),
                                                                                                Uri = file.FileName,
                                                                                                Value = file.Checkpoint.Code
                                                                                            }
                                                                           })
                    .ToList();

                studentsList.Add(new Students
                                 {
                                     Id = dbExerciseGrade.UserToGrade.LdapUid,
                                     Name = dbExerciseGrade.UserToGrade.Name,
                                     Feedback = dbExerciseGrade.Feedback,
                                     Grade = dbExerciseGrade.Grade,
                                     IsGraded = dbExerciseGrade.IsGraded,
                                     Files = studentFilesList
                                 });
            }

            Exercise exercise = new Exercise
                                  {
                                      Author = dbExercise.Author.Name,
                                      Description = dbExercise.Description,
                                      Id = dbExercise.Id,
                                      Name = dbExercise.Name,
                                      Files = filesList,
                                      Students = studentsList,
                                      DueDate = dbExercise.DueDate,
                                      TaskDefinition = dbExercise.TaskDefinition
                                  };

            return CustomResponse.Success(exercise);
        }
    }
}
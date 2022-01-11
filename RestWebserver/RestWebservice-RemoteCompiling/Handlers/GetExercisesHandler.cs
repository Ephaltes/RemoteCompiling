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
            List<Exercise> dbExerciseList = await _exerciseRepository.GetAll();
            List<ExerciseEntity> exerciseList = new List<ExerciseEntity>();

            if (dbExerciseList is null || dbExerciseList.Count == 0)
                return CustomResponse.Success(exerciseList);


            foreach (Exercise item in dbExerciseList)
            {
                List<ExerciseGradeEntity> y = item.HandIns.ConvertAll(x =>
                                                                      {
                                                                          UserEntity user = new UserEntity
                                                                                            {
                                                                                                Email = x.UserToGrade.Email,
                                                                                                Name = x.UserToGrade.Name,
                                                                                                LdapUid = x.UserToGrade.LdapUid,
                                                                                                UserRole = x.UserToGrade.UserRole
                                                                                            };
                                                                          ProjectEntity project = new ProjectEntity
                                                                                                  {
                                                                                                      ExerciseID = x.Exercise.Id,
                                                                                                      Id = x.Project.Id,
                                                                                                      ProjectName = x.Project.ProjectName,
                                                                                                      ProjectType = x.Project.ProjectType,
                                                                                                      StdIn = x.Project.StdIn
                                                                                                  };
                                                                          project.Files = x.Project.Files.ConvertAll(x =>
                                                                                                                     {
                                                                                                                         return new FileEntity
                                                                                                                                {
                                                                                                                                    FileName = x.FileName,
                                                                                                                                    Id = x.Id,
                                                                                                                                    LastModified = x.LastModified,
                                                                                                                                    Checkpoints = new List<CheckPointEntity>
                                                                                                                                                  {
                                                                                                                                                      new CheckPointEntity
                                                                                                                                                      {
                                                                                                                                                          Code = x.Checkpoint.Code,
                                                                                                                                                          Created = x.Checkpoint.Created,
                                                                                                                                                          Id = x.Checkpoint.Id
                                                                                                                                                      }
                                                                                                                                                  }
                                                                                                                                };
                                                                                                                     });

                                                                          return new ExerciseGradeEntity
                                                                                 {
                                                                                     Id = x.Id,
                                                                                     UserToGrade = user,
                                                                                     Grade = x.Grade,
                                                                                     Status = x.Status,
                                                                                     Feedback = x.Feedback,
                                                                                     Project = project
                                                                                 };
                                                                      });
                ExerciseEntity x = new ExerciseEntity
                                   {
                                       Author = item.Author.LdapUid,
                                       Description = item.Description,
                                       Id = item.Id,
                                       Name = item.Name,
                                       Template = item.Template,
                                       HandIns = y,
                                       DueDate = item.DueDate,
                                       TaskDefinition = item.TaskDefinition
                                   };
                exerciseList.Add(x);
            }

            return CustomResponse.Success(exerciseList);
        }
    }
}
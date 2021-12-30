using System.Collections.Generic;
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

            List<ExerciseGradeEntity> y = dbExercise.HandIns.ConvertAll(x =>
                                                                        {
                                                                            UserEntity user = new()
                                                                                              {
                                                                                                  Email = x.UserToGrade.Email,
                                                                                                  Name = x.UserToGrade.Name,
                                                                                                  LdapUid = x.UserToGrade.LdapUid,
                                                                                                  UserRole = x.UserToGrade.UserRole
                                                                                              };
                                                                            ProjectEntity project = new()
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
                                                                                                                                                        new()
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
            ExerciseEntity x = new()
                               {
                                   Author = dbExercise.Author.LdapUid,
                                   Description = dbExercise.Description,
                                   Id = dbExercise.Id,
                                   Name = dbExercise.Name,
                                   Template = dbExercise.Template,
                                   HandIns = y,
                                   DueDate = dbExercise.DueDate,
                                   TaskDefinition = dbExercise.TaskDefinition
                               };

            return CustomResponse.Success(x);
        }
    }
}
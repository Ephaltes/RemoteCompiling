using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Query;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public class GetGradeForStudentInExerciseHandler : BaseHandler<GetGradeForStudentInExerciseQuery, CustomResponse<ExerciseGradeEntity>>
    {
        private readonly IExerciseGradeRepository _exerciseGradeRepository;

        public GetGradeForStudentInExerciseHandler(IUserRepository userRepository, IExerciseGradeRepository exerciseGradeRepository)
            : base(userRepository)
        {
            _exerciseGradeRepository = exerciseGradeRepository;
        }

        public override async Task<CustomResponse<ExerciseGradeEntity>> Handle(GetGradeForStudentInExerciseQuery request, CancellationToken cancellationToken)
        {
            User user = GetUserFromToken(request.Token);

            ExerciseGrade? exerciseGrade = await _exerciseGradeRepository.Get(request.StudentId, request.ExerciseId);

            if (exerciseGrade is null)
                return CustomResponse.Error<ExerciseGradeEntity>(404, "No grade found for this student in this exercise");

            if (exerciseGrade.Status == GradingStatus.NotGraded)
            {
                return CustomResponse.Error<ExerciseGradeEntity>(425, "too early");
            }

            if (exerciseGrade.Status == GradingStatus.InProcess)
            {
                return CustomResponse.Error<ExerciseGradeEntity>(425, "too early, in processing");
            }

            UserEntity userEntity = new()
                                    {
                                        Email = exerciseGrade.UserToGrade.Email,
                                        Name = exerciseGrade.UserToGrade.Name,
                                        LdapUid = exerciseGrade.UserToGrade.LdapUid,
                                        UserRole = exerciseGrade.UserToGrade.UserRole
                                    };
            ProjectEntity project = new()
                                    {
                                        ExerciseID = exerciseGrade.Exercise.Id,
                                        Id = exerciseGrade.Project.Id,
                                        ProjectName = exerciseGrade.Project.ProjectName,
                                        ProjectType = exerciseGrade.Project.ProjectType,
                                        StdIn = exerciseGrade.Project.StdIn
                                    };
            project.Files = exerciseGrade.Project.Files.ConvertAll(x =>
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

            var z = new ExerciseGradeEntity
                    {
                        Id = exerciseGrade.Id,
                        UserToGrade = userEntity,
                        Grade = exerciseGrade.Grade,
                        Status = exerciseGrade.Status,
                        Feedback = exerciseGrade.Feedback,
                        Project = project
                    };


            return CustomResponse.Success(z);
        }
    }
}
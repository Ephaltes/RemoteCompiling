using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public class HandInExerciseHandler : BaseHandler<HandInCommand, CustomResponse<bool>>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IUserRepository _userRepository;

        public HandInExerciseHandler(IExerciseRepository exerciseRepository, IUserRepository userRepository)
            : base(userRepository)
        {
            _exerciseRepository = exerciseRepository;
            _userRepository = userRepository;
        }

        public override async Task<CustomResponse<bool>> Handle(HandInCommand request, CancellationToken cancellationToken)
        {
            Exercise exercise = await _exerciseRepository.Get(request.ExerciseId);
            User? user = await _userRepository.GetUserByLdapUid(request.Token.Claims.First(x => x.Type == ClaimTypes.Sid).Value);
            ExerciseGrade userAlreadyInHandIns = exercise.HandIns.FirstOrDefault(x => x.UserToGrade.LdapUid == user.LdapUid);

            if (userAlreadyInHandIns is not null && userAlreadyInHandIns.Status != GradingStatus.NotGraded)
            {
                throw new Exception("currently in grading or already graded");
            }

            ExerciseGrade? x = new ExerciseGrade
                               {
                                   Exercise = exercise,
                                   Feedback = "",
                                   Grade = -1,
                                   Status = GradingStatus.NotGraded,
                                   UserToGrade = user
                               };

            Project project = user.Projects.FirstOrDefault(x => x.Id == request.ProjectId);

            ExerciseProject? y = new ExerciseProject
                                 {
                                     StdIn = project.StdIn,
                                     ProjectName = project.ProjectName,
                                     ProjectType = project.ProjectType
                                 };

            foreach (File z in project.Files)
            {
                y.Files.Add(new ExerciseFile
                            {
                                User = user,
                                ExerciseGrade = x,
                                Checkpoint = z.Checkpoints.Last(),
                                FileName = z.FileName,
                                LastModified = z.LastModified
                            });
            }

            x.Project = y;

            if (userAlreadyInHandIns is not null)
            {
                userAlreadyInHandIns = x;
            }
            else
            {
                exercise.HandIns.Add(x);
            }

            await _exerciseRepository.Update(exercise);

            return CustomResponse.Success(true);
        }
    }
}
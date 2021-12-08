using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public class HandInExerciseHandler : IRequestHandler<HandInCommand, CustomResponse<bool>>
    {
        private readonly IExerciseGradeRepository _exerciseGradeRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IUserRepository _userRepository;

        public HandInExerciseHandler(IExerciseRepository exerciseRepository, IUserRepository userRepository, IExerciseGradeRepository exerciseGradeRepository)
        {
            _exerciseRepository = exerciseRepository;
            _userRepository = userRepository;
            _exerciseGradeRepository = exerciseGradeRepository;
        }

        public async Task<CustomResponse<bool>> Handle(HandInCommand request, CancellationToken cancellationToken)
        {
            Exercise exercise = _exerciseRepository.Get(request.ExerciseId);
            User? user = _userRepository.GetUserByLdapUid(request.Token.Claims.First(x => x.Type == ClaimTypes.Sid).Value);


            ExerciseGrade? x = new ExerciseGrade
                               {
                                   Exercise = exercise,
                                   Feedback = "",
                                   Grade = -1,
                                   IsGraded = false,
                                   UserToGrade = user
                               };

            foreach (var item in user.Projects)
            {
                if (item.Id != request.ProjectId)
                {
                    continue;
                }

                ExerciseProject? y = new ExerciseProject
                                     {
                                         stdin = item.stdin,
                                         ProjectName = item.ProjectName,
                                         ProjectType = item.ProjectType
                                     };

                foreach (var z in item.Files)
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
            }

            //_exerciseGradeRepository.Add(x);
            exercise.HandIns.Add(x);
            _exerciseRepository.Update(exercise);

            return CustomResponse.Success(true);
        }
    }
}
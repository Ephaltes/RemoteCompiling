using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Repositories;

using Exercise = RestWebservice_RemoteCompiling.Database.Exercise;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public class HandInExerciseHandler : IRequestHandler<HandInCommand, CustomResponse<bool>>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IUserRepository _userRepository;


        public HandInExerciseHandler(IExerciseRepository exerciseRepository, IUserRepository userRepository)
        {
            _exerciseRepository = exerciseRepository;
            _userRepository = userRepository;
        }

        public async Task<CustomResponse<bool>> Handle(HandInCommand request, CancellationToken cancellationToken)
        {
            Exercise exercise = _exerciseRepository.Get(request.ExerciseId);
            User? user = _userRepository.GetUserByLdapUid(request.Token.Claims.First(x => x.Type == ClaimTypes.Sid).Value);

            foreach (string fileName in request.FilesNames)
            {
                File userFile = user.Files.First(x => x.FileName == fileName);

                ExerciseFile exerciseFile = new ExerciseFile
                                            {
                                                User = user,
                                                Exercise = exercise,
                                                Checkpoint = userFile.Checkpoints.Last(),
                                                FileName = userFile.FileName,
                                                FileNodeType = userFile.FileNodeType,
                                                LastModified = userFile.LastModified
                                            };
                exercise.Files.Add(exerciseFile);
            }

            _exerciseRepository.Update(exercise);

            return CustomResponse.Success(true);
        }
    }
}
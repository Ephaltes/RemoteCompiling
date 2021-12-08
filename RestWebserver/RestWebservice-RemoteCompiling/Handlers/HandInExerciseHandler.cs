using System;
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
    public class HandInExerciseHandler : IRequestHandler<HandinCommand, CustomResponse<bool>>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IUserRepository _userRepository;
        

        public HandInExerciseHandler(IExerciseRepository exerciseRepository, IUserRepository userRepository)
        {
            _exerciseRepository = exerciseRepository;
            _userRepository = userRepository;
        }

        public async Task<CustomResponse<bool>> Handle(HandinCommand request, CancellationToken cancellationToken)
        {
            var exercise = _exerciseRepository.Get(request.Id);
            var user = _userRepository.GetUserByLdapUid(request.Token.Claims.First(x => x.Type == ClaimTypes.Sid).Value);

            foreach (var files in request.Files)
            {
                var userFile = user.Files.First(x => x.FileName == files.Name);

                var exerciseFile = new ExerciseFile()
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
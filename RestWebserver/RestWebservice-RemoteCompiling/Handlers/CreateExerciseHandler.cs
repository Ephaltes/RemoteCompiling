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
    public class CreateExerciseHandler : IRequestHandler<CreateExerciseCommand, CustomResponse<int>>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IUserRepository _userRepository;
        public CreateExerciseHandler(IUserRepository userRepository, IExerciseRepository exerciseRepository)
        {
            _userRepository = userRepository;
            _exerciseRepository = exerciseRepository;
        }

        public async Task<CustomResponse<int>> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
        {
            string ldapIdent = request.Token.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            User? ldapUser = _userRepository.GetUserByLdapUid(ldapIdent);
            if (ldapUser is null)
            {
                throw new Exception("ldapuser not found in db");
            }

            Exercise exercise = new Exercise();
            exercise.Description = request.Description;
            exercise.Name = request.Name;
            exercise.Author = ldapUser;
            exercise.TaskDefinition = request.TaskDefinition;

            return CustomResponse.Success(_exerciseRepository.Add(exercise));
        }
    }
}
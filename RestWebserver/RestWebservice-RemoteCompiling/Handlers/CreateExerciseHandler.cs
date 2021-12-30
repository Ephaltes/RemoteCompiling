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
    public class CreateExerciseHandler : BaseHandler<CreateExerciseCommand, CustomResponse<int>>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IUserRepository _userRepository;
        public CreateExerciseHandler(IUserRepository userRepository, IExerciseRepository exerciseRepository)
            : base(userRepository)
        {
            _userRepository = userRepository;
            _exerciseRepository = exerciseRepository;
        }

        public override async Task<CustomResponse<int>> Handle(CreateExerciseCommand request, CancellationToken cancellationToken)
        {
            string ldapIdent = request.Token.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            User? ldapUser = await _userRepository.GetUserByLdapUid(ldapIdent);

            if (ldapUser is null /*||  TODO: ldapUser.UserRole != UserRole.Teacher*/)
            {
                return CustomResponse.Error<int>(403);
            }

            Exercise exercise = new Exercise
                                {
                                    Description = request.Description,
                                    Name = request.Name,
                                    Author = ldapUser,
                                    TaskDefinition = request.TaskDefinition,
                                    Template = request.template
                                };
            exercise.Template.ProjectName = exercise.Name + " Project";
            exercise.Template.ProjectType = request.TemplateProjectType;

            int retVal = await _exerciseRepository.Add(exercise);

            return CustomResponse.Success(retVal);
        }
    }
}
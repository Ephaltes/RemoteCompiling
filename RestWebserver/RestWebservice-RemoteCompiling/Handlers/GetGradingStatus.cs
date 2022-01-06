using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Query;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public class GetGradingStatus : BaseHandler<GetGradingStatusQuery, CustomResponse<GradingStatus>>
    {
        private readonly IExerciseGradeRepository _exerciseGradeRepository;
        private readonly IUserRepository _userRepository;

        public GetGradingStatus(IUserRepository userRepository, IExerciseGradeRepository exerciseGradeRepository)
            : base(userRepository)
        {
            _userRepository = userRepository;
            _exerciseGradeRepository = exerciseGradeRepository;
        }

        public override async Task<CustomResponse<GradingStatus>> Handle(GetGradingStatusQuery request, CancellationToken cancellationToken)
        {
            string ldapIdent = request.Token.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            User? ldapUser = await _userRepository.GetUserByLdapUid(ldapIdent);

            if (ldapUser is null)
            {
                return CustomResponse.Error<GradingStatus>(403);
            }

            ExerciseGrade? exerciseGrade = await _exerciseGradeRepository.Get(request.StudentId, request.ExerciseId);

            return CustomResponse.Success(exerciseGrade.Status);
        }
    }
}
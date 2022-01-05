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
    public class GradeExerciseHandler : BaseHandler<GradeExerciseCommand, CustomResponse<bool>>
    {
        private readonly IExerciseGradeRepository _exerciseGradeRepository;
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IUserRepository _userRepository;

        public GradeExerciseHandler(IExerciseRepository exerciseRepository, IUserRepository userRepository, IExerciseGradeRepository exerciseGradeRepository)
            : base(userRepository)
        {
            _exerciseRepository = exerciseRepository;
            _userRepository = userRepository;
            _exerciseGradeRepository = exerciseGradeRepository;
        }

        public override async Task<CustomResponse<bool>> Handle(GradeExerciseCommand request, CancellationToken cancellationToken)
        {
            string ldapIdent = request.Token.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
            User? ldapUser = await _userRepository.GetUserByLdapUid(ldapIdent);

            if (ldapUser is null /* TODO || ldapUser.UserRole != UserRole.Teacher */)
            {
                return CustomResponse.Error<bool>(403);
            }

            ExerciseGrade obj = await _exerciseGradeRepository.Get(request.StudentId, request.ExerciseId);
            obj.Feedback = request.Feedback ?? obj.Feedback;
            obj.Grade = request.Grading ?? obj.Grade;
            obj.Status = request.Status ?? obj.Status;
            obj.UserToGrade = await _userRepository.GetUserByLdapUid(request.StudentId) ?? throw new Exception("user not found");
            obj.Exercise = await _exerciseRepository.Get(request.ExerciseId) ?? throw new Exception("Exercise not found");

            await _exerciseGradeRepository.Update(obj);

            return CustomResponse.Success(true);
        }
    }
}
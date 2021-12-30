using System;
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
            ExerciseGrade obj = _exerciseGradeRepository.Get(request.ExerciseId);
            obj.Feedback = request.Feedback ?? obj.Feedback;
            obj.Grade = request.Grading ?? obj.Grade;
            obj.Status = request.Status ?? obj.Status;
            obj.UserToGrade = await _userRepository.GetUserByLdapUid(request.StudentId) ?? throw new Exception("user not found");
            obj.Exercise = _exerciseRepository.Get(request.ExerciseId) ?? throw new Exception("Exercise not found");

            _exerciseGradeRepository.Update(obj);

            return CustomResponse.Success(true);
        }
    }
}
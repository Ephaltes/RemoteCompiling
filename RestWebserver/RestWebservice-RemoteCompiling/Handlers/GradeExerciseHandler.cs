using System;
using System.Threading;
using System.Threading.Tasks;

using MediatR;

using RestWebservice_RemoteCompiling.Command;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public class GradeExerciseHandler : IRequestHandler<GradeExerciseCommand, CustomResponse<bool>>
    {
        private readonly IExerciseRepository _exerciseRepository;
        private readonly IUserRepository _userRepository;
        private readonly IExerciseGradeRepository _exerciseGradeRepository;

        public GradeExerciseHandler(IExerciseRepository exerciseRepository, IUserRepository userRepository, IExerciseGradeRepository exerciseGradeRepository)
        {
            _exerciseRepository = exerciseRepository;
            _userRepository = userRepository;
            _exerciseGradeRepository = exerciseGradeRepository;
        }

        public async Task<CustomResponse<bool>> Handle(GradeExerciseCommand request, CancellationToken cancellationToken)
        {
            var obj = _exerciseGradeRepository.Get(request.ExerciseId);
            obj.Feedback = request.Feedback;
            obj.Grade = request.Grading;
            obj.IsGraded = request.Graded;
            obj.UserToGrade = _userRepository.GetUserByLdapUid(request.StudentId) ?? throw new Exception("user not found");
            obj.Exercise = _exerciseRepository.Get(request.ExerciseId) ?? throw new Exception("Exercise not found");

            _exerciseGradeRepository.Update(obj);

            return CustomResponse.Success(true);
        }
    }
}
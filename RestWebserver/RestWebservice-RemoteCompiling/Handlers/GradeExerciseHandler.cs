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

        public async Task<CustomResponse<bool>> Handle(GradeExerciseCommand request, CancellationToken cancellationToken)
        {
            var obj = new Database.ExerciseGrade();
            obj.Feedback = request.Feedback;
            obj.Grade = request.Grading;
            obj.IsGraded = request.Graded;
            obj.UserToGrade = _userRepository.GetUserByLdapUid(request.Student_exercice_id) ?? throw new Exception("user not found");
            obj.Exercise = _exerciseRepository.Get(request.Id) ?? throw new Exception("Exercise not found");

            _exerciseGradeRepository.Add(obj);

            return CustomResponse.Success(true);
        }
    }
}
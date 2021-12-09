using System.Threading;
using System.Threading.Tasks;

using RestWebservice_RemoteCompiling.Database;
using RestWebservice_RemoteCompiling.Entities;
using RestWebservice_RemoteCompiling.Query;
using RestWebservice_RemoteCompiling.Repositories;

namespace RestWebservice_RemoteCompiling.Handlers
{
    public class GetGradeForStudentInExerciseHandler : BaseHandler<GetGradeForStudentInExerciseQuery, CustomResponse<ExerciseGrade>>
    {
        private readonly IExerciseGradeRepository _exerciseGradeRepository;
        public GetGradeForStudentInExerciseHandler(IUserRepository userRepository, IExerciseGradeRepository exerciseGradeRepository)
            : base(userRepository)
        {
            _exerciseGradeRepository = exerciseGradeRepository;
        }
        public override async Task<CustomResponse<ExerciseGrade>> Handle(GetGradeForStudentInExerciseQuery request, CancellationToken cancellationToken)
        {
            User user = GetUserFromToken(request.Token);

            ExerciseGrade? exerciseGrade = _exerciseGradeRepository.Get(request.StudentId, request.ExerciseId);

            if (exerciseGrade is null)
                return CustomResponse.Error<ExerciseGrade>(404,"No grade found for this student in this exercise");

            return CustomResponse.Success(exerciseGrade);
        }
    }
}
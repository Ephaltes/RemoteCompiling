using System.Collections.Generic;
using System.Threading.Tasks;

using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Repositories
{
    public interface IExerciseGradeRepository
    {
        public Task<int> Add(ExerciseGrade exerciseGrade);

        public Task<List<ExerciseGrade>> Get(string studentId);

        public Task<ExerciseGrade?> Get(string studentId, int exerciseId);

        public Task<ExerciseGrade?> Get(int exerciseGradeId);
        public Task<int> Update(ExerciseGrade exerciseGrade);
    }
}
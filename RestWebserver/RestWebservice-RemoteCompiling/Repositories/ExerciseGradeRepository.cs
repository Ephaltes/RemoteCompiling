using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Repositories
{
    public class ExerciseGradeRepository : IExerciseGradeRepository
    {
        private readonly RemoteCompileDbContext _context;
        public ExerciseGradeRepository(RemoteCompileDbContext context)
        {
            _context = context;
        }

        public int Add(ExerciseGrade exerciseGrade)
        {
            _context.ExerciseGrades.AddAsync(exerciseGrade);

            return _context.SaveChanges();
        }
        public int Update(ExerciseGrade exerciseGrade)
        {
            _context.ExerciseGrades.Update(exerciseGrade);

            return _context.SaveChanges();
        }
        public List<ExerciseGrade> Get(string studentId)
        {
            return _context.ExerciseGrades.Where(x => x.UserToGrade.LdapUid == studentId).ToList();
        }

        public ExerciseGrade? Get(string studentId, int exerciseId)
        {
            return _context.ExerciseGrades.FirstOrDefault(x => x.Exercise.Id == exerciseId && x.UserToGrade.LdapUid == studentId);
        }

        public ExerciseGrade Get(int exerciseGradeId)
        {
            return _context.ExerciseGrades.First(x => x.Exercise.Id == exerciseGradeId);
        }
    }
}
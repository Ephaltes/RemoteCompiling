using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

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

        public async Task<int> Add(ExerciseGrade exerciseGrade)
        {
            await _context.ExerciseGrades.AddAsync(exerciseGrade);

            return await _context.SaveChangesAsync();
        }
        public async Task<int> Update(ExerciseGrade exerciseGrade)
        {
            _context.ExerciseGrades.Update(exerciseGrade);

            return await _context.SaveChangesAsync();
        }
        public async Task<List<ExerciseGrade>> Get(string studentId)
        {
            return await _context.ExerciseGrades.Where(x => x.UserToGrade.LdapUid == studentId).ToListAsync();
        }

        public async Task<ExerciseGrade?> Get(string studentId, int exerciseId)
        {
            return await _context.ExerciseGrades.FirstOrDefaultAsync(x => x.Exercise.Id == exerciseId && x.UserToGrade.LdapUid == studentId);
        }

        public async Task<ExerciseGrade?> Get(int exerciseGradeId)
        {
            return await _context.ExerciseGrades.FirstOrDefaultAsync(x => x.Exercise.Id == exerciseGradeId);
        }
    }
}
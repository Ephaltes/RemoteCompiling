using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly RemoteCompileDbContext _context;
        public ExerciseRepository(RemoteCompileDbContext context)
        {
            _context = context;
        }
        public async Task<int> Add(Exercise exercise)
        {
            await _context.Exercises.AddAsync(exercise);
            await _context.SaveChangesAsync();

            return exercise.Id;
        }

        public async Task Update(Exercise exercise)
        {
            _context.Exercises.Update(exercise);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            IDbContextTransaction? transaction = await _context.Database.BeginTransactionAsync();
            Exercise? exercise = await _context.Exercises.FindAsync(id);
            List<ExerciseGrade> gradesToRemove = _context.ExerciseGrades.Where(x => x.Exercise.Id == id).ToList();

            foreach (ExerciseGrade exerciseGrade in gradesToRemove)
            {
                ExerciseFile? filesToRemove = _context.ExerciseFiles.FirstOrDefault(x => x.ExerciseGrade.Id == exerciseGrade.Id);
                _context.ExerciseFiles.Remove(filesToRemove);
            }

            await _context.SaveChangesAsync();
            _context.ExerciseGrades.RemoveRange(gradesToRemove);
            await _context.SaveChangesAsync();
            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        public async Task<Exercise?> Get(int? id)
        {
            return await _context.Exercises.FindAsync(id);
        }

        public async Task<List<Exercise>> GetAll()
        {
            return await _context.Exercises.ToListAsync();
        }
    }
}
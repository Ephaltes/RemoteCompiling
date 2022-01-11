using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

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
            Exercise? exercise = await _context.Exercises.FindAsync(id);
            _context.Exercises.Remove(exercise);
            await _context.SaveChangesAsync();
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
using System.Collections.Generic;
using System.Linq;

using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private RemoteCompileDbContext _context;
        public ExerciseRepository(RemoteCompileDbContext context)
        {
            _context = context;
        }
        public int Add(Exercise exercise)
        {
            _context.Exercises.Add(exercise);
            _context.SaveChanges();
            return exercise.Id;
        }
        
        public void Update(Exercise exercise)
        {
            _context.Exercises.Update(exercise);
            _context.SaveChanges();
        }
        
        public void Delete(int id)
        {
            var exercise = _context.Exercises.Find(id);
            _context.Exercises.Remove(exercise);
            _context.SaveChanges();
        }
        
        public Exercise Get(int id)
        {
            return _context.Exercises.Find(id);
        }
        
        public List<Exercise> GetAll()
        {
            return _context.Exercises.ToList();
        }
    }
}
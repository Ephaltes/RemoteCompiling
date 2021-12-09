using System.Collections.Generic;

using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Repositories
{
    public interface IExerciseRepository
    {
        public int Add(Exercise exercise);
        public void Update(Exercise exercise);

        public void Delete(int id);

        public Exercise Get(int id);

        public List<Exercise> GetAll();
    }
}
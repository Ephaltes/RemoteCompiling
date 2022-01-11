using System.Collections.Generic;
using System.Threading.Tasks;

using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Repositories
{
    public interface IExerciseRepository
    {
        public Task<int> Add(Exercise exercise);
        public Task Update(Exercise exercise);

        public Task Delete(int id);

        public Task<Exercise?> Get(int? id);

        public Task<List<Exercise>> GetAll();
    }
}
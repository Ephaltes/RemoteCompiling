using System.Collections.Generic;
using System.Threading.Tasks;

using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Repositories
{
    public interface ICheckpointRepository
    {
        public Task<List<Checkpoint>> GetAllCheckpoints();
        public Task<Checkpoint?> GetCheckpoint(int id);
        public Task<IReadOnlyCollection<Checkpoint>> GetCheckpointsByFileId(int fileId);
        public Task<Checkpoint?> GetCheckpointsByExerciseFileId(int exerciseFileId);
        public Task<Checkpoint> AddCheckpoint(Checkpoint checkpoint);
        public Task<Checkpoint> UpdateCheckpoint(Checkpoint checkpoint);
        public Task<Checkpoint?> DeleteCheckpoint(int id);
    }
}
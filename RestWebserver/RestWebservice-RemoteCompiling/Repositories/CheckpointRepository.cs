using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Repositories
{
    public class CheckpointRepository : ICheckpointRepository
    {
        private readonly RemoteCompileDbContext _context;
        public CheckpointRepository(RemoteCompileDbContext context)
        {
            _context = context;
        }

        public async Task<List<Checkpoint>> GetAllCheckpoints()
        {
            return await _context.Checkpoints.ToListAsync();
        }

        public async Task<Checkpoint?> GetCheckpoint(int id)
        {
            return await _context.Checkpoints.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IReadOnlyCollection<Checkpoint>> GetCheckpointsByFileId(int fileId)
        {
            File? file = await _context.Files.FirstOrDefaultAsync(c => c.Id == fileId);

            return file is null ? null : file.Checkpoints.ToList();
        }

        public async Task<Checkpoint?> GetCheckpointsByExerciseFileId(int exerciseFileId)
        {
            ExerciseFile? file = await _context.ExerciseFiles.FirstOrDefaultAsync(c => c.Id == exerciseFileId);

            return file?.Checkpoint;
        }

        public async Task<Checkpoint> AddCheckpoint(Checkpoint checkpoint)
        {
            await _context.Checkpoints.AddAsync(checkpoint);
            await _context.SaveChangesAsync();

            return checkpoint;
        }

        public async Task<Checkpoint> UpdateCheckpoint(Checkpoint checkpoint)
        {
            _context.Checkpoints.Update(checkpoint);
            await _context.SaveChangesAsync();

            return checkpoint;
        }
        public async Task<Checkpoint?> DeleteCheckpoint(int id)
        {
            Checkpoint? checkpoint = await _context.Checkpoints.FirstOrDefaultAsync(c => c.Id == id);
            if (checkpoint == null)
            {
                return null;
            }

            _context.Checkpoints.Remove(checkpoint);
            await _context.SaveChangesAsync();

            return checkpoint;
        }
    }
}
using System.Threading.Tasks;

using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Repositories
{
    public interface IFileRepository
    {
        public Task<bool> UserIsOwnerOfFile(string ldapUid, int fileId);
        public Task<File?> GetFile(int fileId);

        public Task Update(File file);
    }
}
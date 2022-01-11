using System;
using System.Threading.Tasks;

using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Repositories
{
    public interface ISessionRepository
    {
        public Task<Session> Add(Session session);
        public Task<Session?> GetSessionByGuid(Guid guid);
        public Task DeleteExpiredSessions();
    }
}
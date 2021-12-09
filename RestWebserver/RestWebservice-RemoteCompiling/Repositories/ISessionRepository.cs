using System;

using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Repositories
{
    public interface ISessionRepository
    {
        public Session Add(Session session);
        public Session? GetSessionByGuid(Guid guid);
        public void DeleteExpiredSessions();
    }
}
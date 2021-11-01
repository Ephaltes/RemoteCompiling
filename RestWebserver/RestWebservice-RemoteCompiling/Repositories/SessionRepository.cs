using System;
using System.Linq;
using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private RemoteCompileDbContext _context;
        public SessionRepository(RemoteCompileDbContext context)
        {
            _context = context;
        }
        public Session Add(Session session)
        {
            _context.Sessions.Add(session);
            _context.SaveChanges();

            return session;
        }
        public Session? GetSessionByGuid(Guid guid)
        {
            return _context.Sessions.FirstOrDefault(x => x.Id.ToString("N") == guid.ToString("N"));
        }
        public void DeleteExpiredSessions()
        {
            foreach (Session session in _context.Sessions)
            {
                if (session.Expiration > DateTime.Now)
                    _context.Sessions.Remove(session);
            }

            _context.SaveChanges();
        }
    }
}
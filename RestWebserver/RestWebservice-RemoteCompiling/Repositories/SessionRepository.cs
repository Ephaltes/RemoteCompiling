using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly RemoteCompileDbContext _context;
        public SessionRepository(RemoteCompileDbContext context)
        {
            _context = context;
        }
        public async Task<Session> Add(Session session)
        {
            await _context.Sessions.AddAsync(session);
            await _context.SaveChangesAsync();

            return session;
        }
        public async Task<Session?> GetSessionByGuid(Guid guid)
        {
            return await _context.Sessions.FirstOrDefaultAsync(x => x.Id.ToString("N") == guid.ToString("N"));
        }
        public async Task DeleteExpiredSessions()
        {
            foreach (Session session in _context.Sessions)
            {
                if (session.Expiration > DateTime.Now)
                    _context.Sessions.Remove(session);
            }

            await _context.SaveChangesAsync();
        }
    }
}
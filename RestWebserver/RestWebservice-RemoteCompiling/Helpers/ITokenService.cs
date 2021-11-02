using RestWebservice_RemoteCompiling.Database;

namespace RestWebservice_RemoteCompiling.Helpers
{
    public interface ITokenService
    {
        string BuildToken(User user);
        bool ValidateToken(string token);
    }
}
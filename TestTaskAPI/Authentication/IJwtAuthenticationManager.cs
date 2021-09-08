using TestTaskAPI.Data;

namespace TestTaskAPI
{
    public interface IJwtAuthenticationManager
    {
        string GetToken(string login, string password);
        string Authenticate(UsersContext usersDbContext, string login, string password);
    }
}

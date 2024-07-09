using Backend.Model;

namespace Backend.Service
{
    public interface IUserService
    {
        Task<User> FindByUsername(string username);
        string CreateToken(string username, string password);
    }
}

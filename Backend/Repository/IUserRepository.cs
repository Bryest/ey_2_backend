using Backend.Model;

namespace Backend.Repository
{
    public interface IUserRepository
    {
        Task<User> GetByUsernameAsync(string username);
    }
}

using ControleDeGastosAPI.Models;

namespace ControleDeGastosAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User> AuthenticateAsync(string email, string senha);
        Task<User> GetAsyncByEmail(string email);
        Task<User> UpdateAsync(User user, string UUID);
        Task<bool> DeleteAsync(string UUID);
    }
}

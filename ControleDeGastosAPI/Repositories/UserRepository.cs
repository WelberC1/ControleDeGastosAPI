using ControleDeGastosAPI.Data;
using ControleDeGastosAPI.Helpers;
using ControleDeGastosAPI.Models;
using ControleDeGastosAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastosAPI.Repositories
{
    //IMPLEMENTAR DTOs no projeto. Autenticação será feita por JWT futuramente.
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;

        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<User> CreateAsync(User user)
        {
            try
            {
                user.Password = PasswordHasher.HashPassword(user.Password);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Erro ao criar o usuário: {ex.Message}");
                throw new Exception("Oops! Tivemos um erro ao criar o usuário! Tente novamente mais tarde.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado, tente novamente mais tarde.");
            }
        }

        public async Task<User> AuthenticateAsync(string email, string senha)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user == null)
                {
                    return null;
                }

                var hashedPassword = PasswordHasher.HashPassword(senha);

                if (user.Password == hashedPassword)
                {
                    return user;
                }

                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        public async Task<User> GetAsyncByEmail(string email)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
                
                if (user == null || !user.IsActive)
                {
                    return null;
                }

                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar usuário por email: {ex.Message}");
                throw new Exception("Ocorreu um erro ao buscar o usuário, tente novamente mais tarde.");
            }
        }


        public async Task<User> UpdateAsync(User user, string UUID)
        {
            try
            {
                var userBeforeUpdate = await _context.Users.FirstOrDefaultAsync(u => u.UUID == UUID);

                if (userBeforeUpdate != null)
                {
                    userBeforeUpdate.Name = user.Name;
                    userBeforeUpdate.Email = user.Email;
                    userBeforeUpdate.Password = user.Password;

                    await _context.SaveChangesAsync();
                    return userBeforeUpdate;
                }

                return null;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Erro ao atualizar o usuário: {ex.Message}");
                throw new Exception("Erro ao atualizar o usuário, tente novamente mais tarde.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado, tente novamente mais tarde.");
            }
        }

        //SOFT DELETE
        public async Task<bool> DeleteAsync(string UUID)
        {
            try
            {
                var user = await _context.Users.FindAsync(UUID);

                if (user == null)
                {
                    return false;
                }

                user.IsActive = false;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"Erro ao deletar o usuário: {ex.Message}");
                throw new Exception("Erro ao deletar o usuário, tente novamente mais tarde.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado, tente novamente mais tarde.");
            }
        }
    }
}

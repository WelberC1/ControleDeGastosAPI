using ControleDeGastosAPI.Enums;
using ControleDeGastosAPI.Models;

namespace ControleDeGastosAPI.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<Transaction> CreateAsync(Transaction transaction);
        Task <List<Transaction>> GetAllTransactionsByUser(string userUUID);
        Task <List<Transaction>> GetTransactionsByRangeDate(DateTime initialDate, DateTime finalDate);
        Task<List<Transaction>> GetTransactionsByCategory(TransactionCategory category);
        Task<List<Transaction>> GetTransactionsByType(TransactionType type);
        Task<Transaction> UpdateAsync(Transaction transaction);
        Task<bool> DeleteAsync(string UUID);
    }
}

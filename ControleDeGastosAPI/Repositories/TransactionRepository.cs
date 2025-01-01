using ControleDeGastosAPI.Data;
using ControleDeGastosAPI.Enums;
using ControleDeGastosAPI.Models;
using ControleDeGastosAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleDeGastosAPI.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public TransactionRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Transaction> CreateAsync(Transaction transaction)
        {
            await _dbContext.Transactions.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();
            return transaction;
        }

        public async Task<bool> DeleteAsync(string UUID)
        {
            var transaction = await _dbContext.Transactions
                                               .FirstOrDefaultAsync(t => t.UUID == UUID);

            if (transaction == null)
                return false;

            _dbContext.Transactions.Remove(transaction);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<Transaction>> GetAllTransactionsByUser(string userUUID)
        {
            return await _dbContext.Transactions
                                   .Where(t => t.UserUUID == userUUID)
                                   .ToListAsync();
        }

        public async Task<List<Transaction>> GetTransactionsByCategory(TransactionCategory category)
        {
            return await _dbContext.Transactions
                                   .Where(t => t.TransactionCategory == category)
                                   .ToListAsync();
        }

        public async Task<List<Transaction>> GetTransactionsByRangeDate(DateTime initialDate, DateTime finalDate)
        {
            return await _dbContext.Transactions
                                   .Where(t => t.Date >= initialDate && t.Date <= finalDate)
                                   .ToListAsync();
        }

        public async Task<List<Transaction>> GetTransactionsByType(TransactionType type)
        {
            return await _dbContext.Transactions
                                   .Where(t => t.TypeTransaction == type)
                                   .ToListAsync();
        }

        public async Task<Transaction> UpdateAsync(Transaction transaction)
        {
            var existingTransaction = await _dbContext.Transactions
                                                     .FirstOrDefaultAsync(t => t.UUID == transaction.UUID);

            if (existingTransaction == null)
            {
                return null;
            }

            existingTransaction.Amount = transaction.Amount;
            existingTransaction.Description = transaction.Description;
            existingTransaction.Date = transaction.Date;
            existingTransaction.TypeTransaction = transaction.TypeTransaction;
            existingTransaction.TransactionCategory = transaction.TransactionCategory;
            existingTransaction.IsVisible = transaction.IsVisible;

            await _dbContext.SaveChangesAsync();
            return existingTransaction;
        }
    }
}

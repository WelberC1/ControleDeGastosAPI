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
            try
            {
                await _dbContext.Transactions.AddAsync(transaction);

                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UUID == transaction.UserUUID);

                if (user != null)
                {
                    if (transaction.TypeTransaction == TransactionType.INCOME)
                    {
                        user.Balance += transaction.Amount;
                    }
                    else if (transaction.TypeTransaction == TransactionType.EXPENSE)
                    {
                        user.Balance -= transaction.Amount;
                    }

                    _dbContext.Users.Update(user);
                }

                await _dbContext.SaveChangesAsync();
                return transaction;
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Erro de atualização no banco de dados: {dbEx.Message}");
                throw new Exception("Erro ao criar a transação. Tente novamente mais tarde.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao criar a transação. Tente novamente mais tarde.");
            }
        }

        public async Task<bool> DeleteAsync(string UUID)
        {
            try
            {
                var transaction = await _dbContext.Transactions
                                                   .FirstOrDefaultAsync(t => t.UUID == UUID);

                if (transaction == null)
                    return false;

                _dbContext.Transactions.Remove(transaction);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Erro de atualização no banco de dados: {dbEx.Message}");
                throw new Exception("Erro ao deletar a transação. Tente novamente mais tarde.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao deletar a transação. Tente novamente mais tarde.");
            }
        }

        public async Task<List<Transaction>> GetAllTransactionsByUser(string userUUID)
        {
            try
            {
                return await _dbContext.Transactions
                                       .Where(t => t.UserUUID == userUUID)
                                       .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter transações: {ex.Message}");
                throw new Exception("Ocorreu um erro ao obter as transações. Tente novamente mais tarde.");
            }
        }

        public async Task<List<Transaction>> GetTransactionsByCategory(TransactionCategory category)
        {
            try
            {
                return await _dbContext.Transactions
                                       .Where(t => t.TransactionCategory == category)
                                       .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter transações por categoria: {ex.Message}");
                throw new Exception("Ocorreu um erro ao obter as transações por categoria. Tente novamente mais tarde.");
            }
        }

        public async Task<List<Transaction>> GetTransactionsByRangeDate(DateTime initialDate, DateTime finalDate)
        {
            try
            {
                return await _dbContext.Transactions
                                       .Where(t => t.Date >= initialDate && t.Date <= finalDate)
                                       .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter transações por data: {ex.Message}");
                throw new Exception("Ocorreu um erro ao obter as transações por data. Tente novamente mais tarde.");
            }
        }

        public async Task<List<Transaction>> GetTransactionsByType(TransactionType type)
        {
            try
            {
                return await _dbContext.Transactions
                                       .Where(t => t.TypeTransaction == type)
                                       .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter transações por tipo: {ex.Message}");
                throw new Exception("Ocorreu um erro ao obter as transações por tipo. Tente novamente mais tarde.");
            }
        }

        public async Task<Transaction> UpdateAsync(Transaction transaction)
        {
            try
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
            catch (DbUpdateException dbEx)
            {
                Console.WriteLine($"Erro de atualização no banco de dados: {dbEx.Message}");
                throw new Exception("Erro ao atualizar a transação. Tente novamente mais tarde.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
                throw new Exception("Ocorreu um erro inesperado ao atualizar a transação. Tente novamente mais tarde.");
            }
        }
    }
}

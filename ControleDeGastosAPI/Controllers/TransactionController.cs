using ControleDeGastosAPI.Models;
using ControleDeGastosAPI.Repositories.Interfaces;
using ControleDeGastosAPI.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ControleDeGastosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] Transaction transaction)
        {
            if (transaction == null)
            {
                return BadRequest("Transação não pode ser nula.");
            }

            try
            {
                var createdTransaction = await _transactionRepository.CreateAsync(transaction);
                return CreatedAtAction(nameof(GetTransactionByUUID), new { UUID = createdTransaction.UUID }, createdTransaction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao criar transação: {ex.Message}");
            }
        }

        [HttpGet("{UUID}")]
        public async Task<IActionResult> GetTransactionByUUID(string UUID)
        {
            try
            {
                var transaction = await _transactionRepository.GetAllTransactionsByUser(UUID);
                if (transaction == null)
                {
                    return NotFound("Transação não encontrada.");
                }

                return Ok(transaction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter transação: {ex.Message}");
            }
        }

        [HttpGet("user/{userUUID}")]
        public async Task<IActionResult> GetAllTransactionsByUser(string userUUID)
        {
            try
            {
                var transactions = await _transactionRepository.GetAllTransactionsByUser(userUUID);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter transações: {ex.Message}");
            }
        }

        [HttpGet("category/{category}")]
        public async Task<IActionResult> GetTransactionsByCategory(TransactionCategory category)
        {
            try
            {
                var transactions = await _transactionRepository.GetTransactionsByCategory(category);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter transações por categoria: {ex.Message}");
            }
        }

        [HttpGet("date-range")]
        public async Task<IActionResult> GetTransactionsByDateRange([FromQuery] DateTime initialDate, [FromQuery] DateTime finalDate)
        {
            try
            {
                var transactions = await _transactionRepository.GetTransactionsByRangeDate(initialDate, finalDate);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter transações por data: {ex.Message}");
            }
        }

        [HttpGet("type/{type}")]
        public async Task<IActionResult> GetTransactionsByType(TransactionType type)
        {
            try
            {
                var transactions = await _transactionRepository.GetTransactionsByType(type);
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao obter transações por tipo: {ex.Message}");
            }
        }

        [HttpPut("{UUID}")]
        public async Task<IActionResult> UpdateTransaction(string UUID, [FromBody] Transaction transaction)
        {
            if (transaction == null || transaction.UUID != UUID)
            {
                return BadRequest("Dados inválidos ou UUID não corresponde.");
            }

            try
            {
                var updatedTransaction = await _transactionRepository.UpdateAsync(transaction);
                if (updatedTransaction == null)
                {
                    return NotFound("Transação não encontrada.");
                }

                return Ok(updatedTransaction);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao atualizar transação: {ex.Message}");
            }
        }

        [HttpDelete("{UUID}")]
        public async Task<IActionResult> DeleteTransaction(string UUID)
        {
            try
            {
                var isDeleted = await _transactionRepository.DeleteAsync(UUID);
                if (!isDeleted)
                {
                    return NotFound("Transação não encontrada.");
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao deletar transação: {ex.Message}");
            }
        }
    }
}

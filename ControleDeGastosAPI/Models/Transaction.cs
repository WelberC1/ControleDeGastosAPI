using ControleDeGastosAPI.Enums;

namespace ControleDeGastosAPI.Models
{
    public class Transaction
    {
        public string ?UUID { get; set; }
        public string UserUUID{ get; set; }
        public decimal Amount { get; set; }
        public string ?Description { get; set; }
        public DateTime Date { get; set; }
        public TransactionType TypeTransaction { get; set; }
        public TransactionCategory TransactionCategory { get; set; }
        public bool IsVisible { get; set; } = true;
    }
}

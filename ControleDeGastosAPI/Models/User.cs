using ControleDeGastosAPI.Enums;

namespace ControleDeGastosAPI.Models
{
    public class User
    {
        public string ?UUID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; } = true;

    }
}

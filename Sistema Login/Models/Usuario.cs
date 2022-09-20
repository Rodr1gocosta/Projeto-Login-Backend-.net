
using System.ComponentModel.DataAnnotations;

namespace Sistema_Login.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }

        public string Status { get; set; }

        public bool Ativo { get; set; }
    }
}

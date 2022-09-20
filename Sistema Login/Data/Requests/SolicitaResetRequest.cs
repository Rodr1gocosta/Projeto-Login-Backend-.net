using System.ComponentModel.DataAnnotations;

namespace Sistema_Login.Data.Requests
{
    public class SolicitaResetRequest
    {
        [Required]
        public string Email { get; set; }
    }
}

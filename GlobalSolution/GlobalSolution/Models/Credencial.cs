using Microsoft.Build.Framework;

namespace GlobalSolution.Models
{
    public class Credencial
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Senha { get; set; }

    }
}

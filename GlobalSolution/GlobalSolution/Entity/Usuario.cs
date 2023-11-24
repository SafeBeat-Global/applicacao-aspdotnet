using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalSolution.Entity;

public class Usuario
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    [Required]
    public string Nome { get; set; }

    public string Telefone { get; set; }

    public string TipoUsuario { get; set; }

    [Required]
    public string Email { get; set; }
    [Required]
    public string Senha { get; set; }
}

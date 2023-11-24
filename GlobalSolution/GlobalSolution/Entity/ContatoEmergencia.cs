using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalSolution.Entity;

public class ContatoEmergencia
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    public string Nome { get; set; }

    [Required]
    public string Telefone { get; set; }

    [Required]
    [ForeignKey("Paciente")]
    public long IdPaciente { get; set; }
    public Paciente? Paciente { get; set; }
}
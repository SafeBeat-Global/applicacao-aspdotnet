using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalSolution.Entity;

public class DadosIoT
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [ForeignKey("Paciente")]
    public long IdPaciente { get; set; }
    public Paciente Paciente { get; set; }

    [Required]
    public string BatimentosPorMinuto { get; set; }

    [Required]
    public string Agitacao { get; set; }
}
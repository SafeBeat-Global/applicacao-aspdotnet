using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalSolution.Entity;

public class Monitor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [ForeignKey("Usuario")]
    public long IdUsuario { get; set; }
    public Usuario? Usuario { get; set; }

    [ForeignKey("Instituicao")]
    public long? IdInstituicao { get; set; }
    public Instituicao? Instituicao { get; set; }

    [NotMapped]
    [Required(ErrorMessage = "Pelo menos um paciente é obrigatório.")]
    public List<long>? PacienteId { get; set; }
    public List<MonitorPaciente>? MonitorPacientes { get; set; }
//    [Required]
//    public List<Paciente> Pacientes { get; set; }
}
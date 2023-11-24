using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalSolution.Entity;
public class MonitorPaciente
{
    [ForeignKey("MonitorId")]
    public long MonitorId { get; set; }
    public Monitor Monitor { get; set; }
    [ForeignKey("PacienteId")]
    public long PacienteId { get; set; }
    public Paciente Paciente { get; set; }
}


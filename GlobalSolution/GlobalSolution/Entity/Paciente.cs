using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalSolution.Entity;

public class Paciente
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [ForeignKey("Usuario")]
    public long UsuarioId { get; set; }
    public Usuario? Usuario { get; set; }

    public List<ContatoEmergencia>? ContatosEmergencia { get; set; }

    public List<MonitorPaciente>? MonitorPacientes { get; set; }
    //    public List<Monitor>? Monitors { get; set; }

    public List<DadosIoT>? DadosIoTList { get; set; }

    [Required]
    [Range(0, 150)]
    public int Idade { get; set; }

    [Required]
    [RegularExpression("^(masculino|feminino)$", ErrorMessage = "O sexo biológico deve ser 'masculino' ou 'feminino'")]
    public string SexoBiologico { get; set; }

    [Required]
    [MaxLength(20)]
    public string Genero { get; set; }

    [Required]
    [MaxLength(20)]
    public string Colesterol { get; set; }

    [Required]
    [MaxLength(20)]
    public string Triglicerol { get; set; }

    [Required]
    [MaxLength(20)]
    public string Diabete { get; set; }

    public bool Fumante { get; set; }

    public bool Obeso { get; set; }

    public bool ConsumoAlcool { get; set; }

    [Required]
    public string Dieta { get; set; }

    [Required]
    public string UsoMedicamentos { get; set; }
}
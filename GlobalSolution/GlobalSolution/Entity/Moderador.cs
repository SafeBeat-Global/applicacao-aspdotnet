using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalSolution.Entity;

public class Moderador
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [ForeignKey("Usuario")]
    public long IdUsuario { get; set; }
    public Usuario Usuario { get; set; }

    public List<Instituicao> Instituicoes { get; set; }
}
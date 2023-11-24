using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GlobalSolution.Entity;

public class Instituicao
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public List<Monitor> Monitores { get; set; }

    [Required]
    [ForeignKey("Moderador")]
    public long IdModerador { get; set; }
    public Moderador Moderador { get; set; }

    [Required]
    [MaxLength(20)]
    public string Telefone { get; set; }

    [Required]
    public string Endereco { get; set; }
}
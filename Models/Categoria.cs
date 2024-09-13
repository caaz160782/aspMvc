using System.ComponentModel.DataAnnotations;

namespace AspMvcNet.Models;

public class Categoria {
    [Key]
    public int? Id {get; set;}
    [Required(ErrorMessage ="el nombre es obligatoria")]
    public string? Nombre {get; set;}
    public string? Slug {get; set;}
}
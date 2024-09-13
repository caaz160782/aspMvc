using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AspMvcNet.Models;

public class Producto {
    [Key]
    public int? Id {get; set;}
    [Required(ErrorMessage ="el nombre es obligatoria")]
    public string? Nombre {get; set;}    
    public string? Slug {get; set;}
    [Display(Name ="Descripcion")]
    [Required(ErrorMessage ="cescripcion es obligatoria")]
    public string? Descripcion {get; set;}
    [Range(10,10000000,
     ErrorMessage ="cescripcion es obligatoria")]
    public int? Precio {get; set;}
    public int? Stock {get; set;}
    public DateTime? Fecha {get; set;}
    [Display(Name ="Categoria")]
    [Required(ErrorMessage ="categoria es obligatoria")]
    public int? CategoriaId {get; set;}
    [ForeignKey("Categoria")]
    public virtual Categoria? Categoria {get; set;}
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AspMvcNet.Models;

public class ProductoFoto {
    [Key]
    public int Id {get; set;}
    [DataType(DataType.ImageUrl)]
    [Display(Name="Foto")]
    public string? Nombre {get; set;}    
    public int? ProductoId {get; set;}
    [ForeignKey("Categoria")]
    public virtual Producto? Producto {get; set;}
}
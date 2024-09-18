using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AspMvcNet.Models;
using AspMvcNet.Data;
using AspMvcNet.Pagination;
using Slugify; //dotnet add package Slugify 

namespace AspMvcNet.Controllers;

public class EntityController : Controller
{
    private readonly ApplicationDbContext _contexto;

    public EntityController(ApplicationDbContext contexto)
    {
        _contexto= contexto;
    }
    [Route("/entity")]
    public IActionResult Index()
    {
        return View();
    }
    [Route("/entity/categorias")]
    public async Task<IActionResult> CategoriasListar()
    {
        return View(await _contexto.Categorias.OrderByDescending(c=>c.Id).ToListAsync());
    }
    [Route("/entity/categorias/add")]
    public IActionResult CategoriasAdd()
    {
        return View();
    }
    
    [HttpPost] 
    [Route("/entity/categorias/add")]
    [ValidateAntiForgeryToken] 
    public async Task<IActionResult> CategoriasAdd(Categoria model)
    {
         if(ModelState.IsValid){
           Categoria categoria = new Categoria{Nombre=model.Nombre, Slug=new SlugHelper().GenerateSlug(model.Nombre)};
           _contexto.Categorias.Add(categoria);
           await _contexto.SaveChangesAsync();
           return RedirectToAction(nameof(CategoriasAdd));
        }
        return View();
    }

  [Route("/entity/categorias/edit/{id}")]
    public async Task<IActionResult> CategoriaEdit(int id)
    {
        // Buscar la categoría usando el ID
        var categoria = await _contexto.Categorias.FirstOrDefaultAsync(c => c.Id == id);
        
        // Verificar si la categoría fue encontrada
        if (categoria == null)
        {
            return NotFound();
        }

        // Retornar la vista con la categoría
        return View(categoria);
    }
[HttpPost]
[Route("/entity/categorias/edit/{id}")]
public async Task<IActionResult> CategoriaEdit(int id, Categoria model)
{
    if (ModelState.IsValid)
    {
        // Buscar la categoría en la base de datos
        var categoria = await _contexto.Categorias.FindAsync(id);
        // Verificar si la categoría existe
        if (categoria == null)
        {
            return NotFound();
        }

        // Actualizar los campos necesarios
        categoria.Nombre = model.Nombre;
        categoria.Slug = new SlugHelper().GenerateSlug(model.Nombre);
        // Guardar los cambios en la base de datos
        await _contexto.SaveChangesAsync();

        // Redirigir a una vista adecuada, por ejemplo, la lista de categorías
        return RedirectToAction(nameof(CategoriaEdit));        
    }
    // Si el modelo no es válido, retornar la vista con el modelo para mostrar errores
    return View(model);
}
 [Route("/entity/categorias/delete/{id}")]
public async Task<IActionResult> Delete(int id)
{
    // Buscar la categoría en la base de datos
    var categoria = await _contexto.Categorias.FindAsync(id);

    // Verificar si la categoría existe
    if (categoria == null)
    {
        return NotFound();
    }

    // Eliminar la categoría
    _contexto.Categorias.Remove(categoria);
    
    // Guardar los cambios en la base de datos
    await _contexto.SaveChangesAsync();

    // Redirigir a la lista de categorías después de eliminar
    return RedirectToAction(nameof(CategoriasAdd));
}
[Route("/entity/productos")]
    public async Task<IActionResult> ProductosListar()
    {
        var products =await _contexto
                            .Productos
                            .Include(x=>x.Categoria)
                            .OrderByDescending(c=>c.Id)
                            .ToListAsync();
        return View(products);
    }
[Route("/entity/productos-pagination")]
    public async Task<IActionResult> ProductosPaginacion(string currentFilter, string searchString, int? pageNumber )
    {
        if(searchString != null)
        {
            pageNumber=1;
        }else{
            searchString= currentFilter;
        }
      
      ViewData["CurrentFilter"]=searchString;
      ViewData["pageNumber"]=pageNumber;
      //linq
      var productos = from p in _contexto.Productos select p;
      productos=productos.OrderByDescending(s => s.Id)
                         .Include(x => x.Categoria);
      int pageSize=3;
      return View(await PaginatedList<Producto>.CreateAsync(productos.AsNoTracking(),pageNumber?? 1 ,pageSize));

    }


}
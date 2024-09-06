using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AspMvcNet.Models;
//excel
using ClosedXML.Excel;
using System.Data;

namespace AspMvcNet.Controllers;

public class UtilitiesController : Controller
{
    [Route("/Utilities")]
    public IActionResult Index()
    {     
        return View();
    }
    [Route("/Utilities/example")]
    public IActionResult UtilidadesExample()
    {     
        return View();
    }
    [Route("/Utilities/QR")]
    public IActionResult UtilidadesQr()
    {     
        ViewBag.Text="www.google.com";
        return View();
    }
    private List<Categoria> categorias = new List<Categoria>{
        new Categoria {Id= 1, Nombre ="Categoria 1" ,Slug="cat-1"},
        new Categoria {Id= 2, Nombre ="Categoria 2" ,Slug="cat-2"},
        new Categoria {Id= 3, Nombre ="Categoria 3" ,Slug="cat-3"},
        new Categoria {Id= 4, Nombre ="Categoria 4" ,Slug="cat-4"}
    };
    [Route("/Utilities/excel")]
    public async Task<FileResult> UtilidadesExcel()
    {     
       long timeStamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
       var nameFile=$"reporte_{timeStamp}.xlsx";
       return GenerarExcel(nameFile,categorias);
    }
    private FileResult GenerarExcel(string nameFile, IEnumerable<Categoria> categorias){
    DataTable dataTable = new DataTable("categorias");
    dataTable.Columns.AddRange(
        new DataColumn[]{
            new DataColumn("id"),
            new DataColumn("Nombre"),
            new DataColumn("Slug")
        }
    );
    
    foreach(var categoria in categorias)
    {
        dataTable.Rows.Add(categoria.Id, categoria.Nombre, categoria.Slug);
    }

    using (XLWorkbook wb = new XLWorkbook())
    {
        wb.Worksheets.Add(dataTable);
        using (MemoryStream stream = new MemoryStream())
        {
            wb.SaveAs(stream);
            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nameFile);
        }
    }
}
}


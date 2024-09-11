using Microsoft.AspNetCore.Mvc;
using AspMvcNet.Models;
//API
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace AspMvcNet.Controllers;

public class ClientApiController : Controller
{
    public string _token;

    public ClientApiController(){
        //se elimina para subirlo a git
        // _token="Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJpZCI6MzYsImlhdCI6MTcyNjAyNTgwMCwiZXhwIjoxNzI4NjE3ODAwfQ.ZhQYNm5ggBraaGsqDXXHdwBfkTKsu2AfW-7JRj3KtHQ";
    }

    [Route("/ClientAPI")]   
    public async Task<IActionResult> Index()
    {
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization",_token) ;
        var result =await client.GetAsync("https://www.api.tamila.cl/api/categorias");
        var resultBody= await result.Content.ReadAsStringAsync();
        List<Categoria> datos = JsonConvert.DeserializeObject<List<Categoria>>(resultBody);
       // Console.WriteLine(resultBody);
        return View(datos);
    }
    
   [Route("/ClientAPI/add")]   
    public IActionResult Add()
    {
        return View();
    }

   [HttpPost] 
   [Route("/ClientAPI/add")]   
    public async Task<IActionResult> Add(Categoria model)
    {
        if(ModelState.IsValid){
           HttpClient client = new HttpClient();
           var data= new {
            nombre =model.Nombre
           };
           client.DefaultRequestHeaders.Add("Authorization",_token) ;
           var result =await client.PostAsJsonAsync("https://www.api.tamila.cl/api/categorias",data);
           return RedirectToAction(nameof(Add));
        }
        return View();
    }

    [Route("/ClientAPI/edit/{id}")]   
     public async Task<IActionResult> Edit(int id)
    {
        if(id == null){
            return NotFound();
        }
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization",_token) ;
        var result =await client.GetAsync("https://www.api.tamila.cl/api/categorias/"+id);
        string resultBody= await result.Content.ReadAsStringAsync();
        Categoria categoria = JsonConvert.DeserializeObject<Categoria>(resultBody);

        if(categoria == null){
            return NotFound();
        }
        return View(categoria);
    }

    [HttpPost] 
    [Route("/ClientAPI/edit/{id}")]    
    public async Task<IActionResult> Edit(Categoria model)
    { 
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization",_token) ;
        if(ModelState.IsValid){
          var data= new {
            nombre =model.Nombre
           };        
          var result =await client.PutAsJsonAsync("https://www.api.tamila.cl/api/categorias/"+model.Id, data);
          return RedirectToAction(nameof(Edit));
        }
        var result1 =await client.GetAsync("https://www.api.tamila.cl/api/categorias/"+model.Id);
        string resultBody= await result1.Content.ReadAsStringAsync();
        Categoria categoria = JsonConvert.DeserializeObject<Categoria>(resultBody);

        if(categoria == null){
            return NotFound();
        }
        return View();
    }
 

     [Route("/ClientAPI/delete/{id}")]   
     public async Task<IActionResult> Delete(int id)
    {
        if(id == null){
            return NotFound();
        }
        HttpClient client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization",_token) ;
        var result =await client.DeleteAsync("https://www.api.tamila.cl/api/categorias/"+id);
         return RedirectToAction(nameof(Index));
    }
}
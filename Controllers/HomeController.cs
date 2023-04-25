using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using appPetech.Models;
using appPetech.Data;
using Microsoft.EntityFrameworkCore;



namespace appPetech.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _dbcontext;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger,
    ApplicationDbContext context)
    {
        _dbcontext = context;
        _logger = logger;
    }

    public IActionResult Index()
    {
        ObtenerProductos();
        return View();
    }

    public IActionResult Nutricion()
    {
        return View();
    }

    
    public IActionResult Bienestar()
    {
        return View();
    }

    public IActionResult Estilodevida()
      {
        return View();
    }


    public IActionResult ObtenerProductos()
    {
        var models = new List<Producto>{
            new Producto {Name ="Dispensador de galletas"   ,Precio=20.50M, ImageName ="producto.png",PorcentajeDescuento=0.6M},
            new Producto {Name ="Casa para perros"   ,Precio=80.75M, ImageName ="producto.png",PorcentajeDescuento=0.6M},
            new Producto {Name ="Regadera para perros"   ,Precio=65.20M, ImageName ="producto.png",PorcentajeDescuento=0.6M},
            new Producto {Name ="Caja de arena para gatos"   ,Precio=92.34M, ImageName ="producto.png",PorcentajeDescuento=0.6M},
            new Producto {Name ="Cama para gatos"   ,Precio=78.97M, ImageName ="producto.png",PorcentajeDescuento=0.6M},
            new Producto {Name ="Puntero laser"   ,Precio=30.20M, ImageName ="producto.png",PorcentajeDescuento=0.6M},
            new Producto {Name ="Collarin"   ,Precio=14.05M, ImageName ="collarin.jpeg",PorcentajeDescuento=0.6M}
        };
        return View("~/Views/Home/Index.cshtml",models);
    }

    public async Task<IActionResult> BuscarProductos(string? searchString)
        {
            
            var productos = from o in _dbcontext.DataProductos select o;
            //SELECT * FROM t_productos -> &
            if(!String.IsNullOrEmpty(searchString)){
                productos = productos.Where(s => s.Name.Contains(searchString)); //Algebra de bool
                // & + WHERE name like '%ABC%'
            }
            productos = productos.Where(s => s.Status.Contains("Activo"));
            
            return View(await productos.ToListAsync());
        }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

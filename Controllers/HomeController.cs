using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using appPetech.Models;
using appPetech.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;



namespace appPetech.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger,
    ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _logger = logger;   
        _userManager = userManager; 
    }

    public async Task<IActionResult> Index(string searchString)
    {

        var productos = await ObtenerProductos2(searchString);
        var userID = _userManager.GetUserName(User);
            if(userID == null){
                return View(productos);
            }else{
                return RedirectToAction("Index","Catalogo");
            }

    }



    public async Task<List<Producto>> ObtenerProductos2(string searchString)
    {
        var productos = from o in _context.DataProductos select o;

        if(!String.IsNullOrEmpty(searchString)){
            productos = productos.Where(s => s.Name.Contains(searchString));
        }

        return await productos.ToListAsync();
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
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

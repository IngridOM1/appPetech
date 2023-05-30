using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using appPetech.Models;
using appPetech.Data; 
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace appPetech.Controllers
{
    public class CatalogoController : Controller
    {
        
            
        private readonly ILogger<CatalogoController> _logger;

        private readonly ApplicationDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;

        public CatalogoController(ApplicationDbContext context, ILogger<CatalogoController> logger, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _logger = logger;   
            _userManager = userManager;   
        }

        public async Task<IActionResult> Index(string? searchString)
        {
            var productos = await ObtenerProductosCatalogo(searchString);
            var user = await _userManager.GetUserAsync(User);
            var roles = await _userManager.GetRolesAsync(user);

            if(roles.Count>0 && roles[0] == "admin"){
                return RedirectToAction("Index","Admin");
            }else{
                return View(productos);
            }

            //return View(productos);



        }
        
        public async Task<List<Producto>> ObtenerProductosCatalogo(string searchString)
    {
        var productos = from o in _context.DataProductos select o;

        if(!String.IsNullOrEmpty(searchString)){
            productos = productos.Where(s => s.Name.Contains(searchString));
        }

        return await productos.ToListAsync();
    }

        public async Task<IActionResult> Details(int? id){
            Producto objProd = await _context.DataProductos.FindAsync(id);

            if(objProd == null){
                return NotFound();
            }
            return View(objProd);
        }

        public async Task<IActionResult> Add(int? id){
            var userID = _userManager.GetUserName(User);
            if(userID == null){
                ViewData["Message"] = "Debe Iniciar Sesion antes de agregar un producto";
                List<Producto> productos = new List<Producto>();
                return View("Index", productos);
            }else{
                var producto = await _context.DataProductos.FindAsync(id);
                Cart cart = new Cart();
                cart.Producto = producto;
                cart.Precio = producto.Precio;
                cart.Cantidad = 1;
                cart.UserID = userID;
                _context.Add(cart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
        }



    }
}
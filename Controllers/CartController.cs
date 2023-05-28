using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using appPetech.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Dynamic;
using appPetech.Models;


namespace appPetech.Controllers
{
    public class CartController : Controller
    {
        private readonly ILogger<CartController> _logger;
        private readonly ApplicationDbContext _dbcontext;
        private readonly UserManager<IdentityUser> _userManager;

        public CartController(
            ILogger<CartController> logger,
            ApplicationDbContext context,
            UserManager<IdentityUser> userManager
            )
        {
            _logger = logger;
            _dbcontext = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            
            var userIDSession = _userManager.GetUserName(User);

            //SELECT * FROM Proforma p,Producto pr WHERE p.productId=pr.Id And p.UserId=? And p.status='PENDIENTE' 
            var items = from o in _dbcontext.DataCart select o;
            items = items.Include(p => p.Producto).
                    Where(w => w.UserID.Equals(userIDSession) &&
                     w.Status.Equals("PENDIENTE"));
            var itemsCarrito = items.ToList();
            var total = itemsCarrito.Sum(c => c.Cantidad * c.Precio);

            //MEMORIA
            dynamic model = new ExpandoObject();
            model.montoTotal = total;
            model.elementosCarrito = itemsCarrito;

            //Carrito carrito = new Carrito();
            //carrito.total = total;
            //carrito.itemsCarrito = itemsCarrito;

            //return View(carrito);
            return View(model);
        }
        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemcart = await _dbcontext.DataCart.FindAsync(id);
            if (itemcart == null)
            {
                return NotFound();
            }
            return View(itemcart);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Cantidad,Precio,UserID")] Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _dbcontext.Update(cart);
                    await _dbcontext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_dbcontext.DataCart.Any(e => e.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(cart);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cart = await _dbcontext.DataCart.FindAsync(id);
            _dbcontext.DataCart.Remove(cart);
            await _dbcontext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }        


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}

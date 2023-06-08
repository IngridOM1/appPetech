using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Identity;

using appPetech.Data;
using appPetech.Models;
using Microsoft.EntityFrameworkCore;

namespace appPetech.Controllers
{
    public class PedidoController : Controller
    {
        private readonly ILogger<PedidoController> _logger;

        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public PedidoController(ILogger<PedidoController> logger, UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
        }

        public  async Task<IActionResult>  Index(string? searchUsername,string? orderStatus)
        {
            var pedidos = from o in _context.DataPedido select o;

            var userID = _userManager.GetUserName(User);
            //var userName = await _userManager.FindByNameAsync(userID);
            //var rolList = await _userManager.GetRolesAsync(userName);

            //Console.WriteLine(rolList[0]);

            if(!String.IsNullOrEmpty(searchUsername) && !String.IsNullOrEmpty(orderStatus)){
                pedidos = pedidos.Where(s => s.UserID.Contains(searchUsername) && s.Status.Contains(orderStatus));
            }else if (!String.IsNullOrEmpty(searchUsername) ){
                pedidos = pedidos.Where(s => s.UserID.Contains(searchUsername));
            }else if (!String.IsNullOrEmpty(orderStatus)){
                pedidos = pedidos.Where(s => s.Status.Contains(orderStatus));
            }else{
                pedidos = from o in _context.DataPedido select o;
            }



            return View(await pedidos.ToListAsync());
        }

    //[HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(int? id){

        if (id == null || _context.DataPedido == null){
            return NotFound();
        }

        var pedido = await _context.DataPedido.FindAsync(id);

        if (pedido == null){
            return NotFound();
        }
        
        return View(pedido);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("ID,UserID,Total,Status")] Pedido pedido)
        {
            if (id != pedido.ID)
            {
                Console.WriteLine("NO SE LOGRO EDITAR EL DATO ID");
                Console.WriteLine(id);
                Console.WriteLine(""+pedido.ID +pedido.Total + pedido.Status );
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try{
                    _context.Update(pedido);
                    Console.WriteLine("SE LOGRO EDITAR EL DATO");
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException){
                    if (!DataExists(pedido.ID))
                    {
                        Console.WriteLine("NO SE LOGRO EDITAR EL DATO , NO HAY DATAEXIST");
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            Console.WriteLine("NO SE LOGRO EDITAR EL DATO");
            Console.WriteLine(ModelState.IsValid);
            return View("Index",pedido);
        }

        private bool DataExists(int id)
        {
          return (_context.DataPedido?.Any(e => e.ID == id)).GetValueOrDefault();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
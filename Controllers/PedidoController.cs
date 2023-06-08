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
             //var pedidos = from o in _context.DataPedido select o;
             //pedidos = pedidos.Where(s => s.Status.Contains("PENDIENTE"));
            var pedidos = from o in _context.DataPedido select o;

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
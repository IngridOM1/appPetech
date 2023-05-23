using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using appPetech.Models;
using appPetech.Data;

namespace appPetech.Controllers;

public class DeliveryController : Controller
{
    private readonly ApplicationDbContext _context;

    public DeliveryController(ApplicationDbContext context)
        {
            _context = context;
        }


   // listar deliverys
   public async Task<IActionResult> Index()
        {
              return _context.DataDelivery != null ? 
                          View(await _context.DataDelivery.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.DataDelivery'  is null.");
        }

     // GET: Producto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DataDelivery == null)
            {
                return NotFound();
            }

            var deliverys = await _context.DataDelivery
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deliverys == null)
            {
                return NotFound();
            }

            return View(deliverys);
        }



    // crear nuevo delivery
    public IActionResult Create()
    {
        return View();
    }

    // guardar delivery
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Nombre,ApellidoPaterno,ApellidoMaterno,Dni,Celular,Vehiculo,Placa")] Delivery delivery)
    {
        if (ModelState.IsValid)
        {
            _context.Add(delivery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(delivery);
    }




    public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DataDelivery == null)
            {
                return NotFound();
            }

            var delivery = await _context.DataDelivery.FindAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }
            return View(delivery);
        }

        // POST: Producto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,ApellidoPaterno,ApellidoMaterno,Dni,Celular,Vehiculo,Placa")] Delivery delivery)
        {
            if (id != delivery.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(delivery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosExists(delivery.Id))
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
            return View(delivery);
        }

        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DataDelivery == null)
            {
                return NotFound();
            }

            var delivery = await _context.DataDelivery
                .FirstOrDefaultAsync(m => m.Id == id);
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DataDelivery == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DataProducto'  is null.");
            }
            var deliverys = await _context.DataDelivery.FindAsync(id);
            if (deliverys != null)
            {
                _context.DataDelivery.Remove(deliverys);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosExists(int id)
        {
          return (_context.DataDelivery?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using appPetech.Data;
using appPetech.Models;

namespace appPetech.Controllers
{
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Producto
        public async Task<IActionResult> Index(int? page, string sort, string search)
        {
            int pageSize = 10; // Cantidad de elementos por página
            int pageNumber = page ?? 1; // Número de página actual, si no se especifica, es la primera página

            var productos = _context.DataProductos.AsQueryable();
            var totalItems = await productos.CountAsync();
if (!string.IsNullOrEmpty(search))
    {
        productos = productos.Where(p => p.Name.Contains(search));
    }
if (!string.IsNullOrEmpty(search))
            {
                productos = productos.Where(p => p.Name.Contains(search));
            }
            // Ordenar los productos según el parámetro de consulta "sort"
            switch (sort)
            {
                case "id_desc":
                    productos = productos.OrderByDescending(p => p.Id);
                    break;
                default:
                    productos = productos.OrderBy(p => p.Id);
                    break;
            }

            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            ViewBag.CurrentPage = pageNumber;

            var paginatedProductos = await productos.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        
            return View(paginatedProductos);
        }

        // GET: Producto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DataProductos == null)
            {
                return NotFound();
            }

            var productos = await _context.DataProductos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // GET: Producto/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Producto/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Descripcion,Precio,PorcentajeDescuento,ImageName")] Producto productos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productos);
        }

        // GET: Producto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DataProductos == null)
            {
                return NotFound();
            }

            var productos = await _context.DataProductos.FindAsync(id);
            if (productos == null)
            {
                return NotFound();
            }
            return View(productos);
        }

        // POST: Producto/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Marca,Descripcion,Precio,PorcentajeDescuento,ImageName,Status")] Producto productos)
        {
            if (id != productos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductosExists(productos.Id))
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
            return View(productos);
        }

        // GET: Producto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DataProductos == null)
            {
                return NotFound();
            }

            var productos = await _context.DataProductos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productos == null)
            {
                return NotFound();
            }

            return View(productos);
        }

        // POST: Producto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DataProductos == null)
            {
                return Problem("Entity set 'ApplicationDbContext.DataProducto' is null.");
            }
            var productos = await _context.DataProductos.FindAsync(id);
            if (productos != null)
            {
                _context.DataProductos.Remove(productos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductosExists(int id)
        {
            return (_context.DataProductos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

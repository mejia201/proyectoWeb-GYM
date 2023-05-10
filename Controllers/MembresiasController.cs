using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using proyectoWeb_GYM.Models;

namespace proyectoWeb_GYM.Controllers
{
    public class MembresiasController : Controller
    {
        private readonly gymDbContext _context;

        public MembresiasController(gymDbContext context)
        {
            _context = context;
        }

        // GET: Membresias
        public async Task<IActionResult> Index()
        {
              return _context.Membresia != null ? 
                          View(await _context.Membresia.ToListAsync()) :
                          Problem("Entity set 'gymDbContext.Membresia'  is null.");
        }

        // GET: Membresias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Membresia == null)
            {
                return NotFound();
            }

            var membresia = await _context.Membresia
                .FirstOrDefaultAsync(m => m.id_membresia == id);
            if (membresia == null)
            {
                return NotFound();
            }

            return View(membresia);
        }

        // GET: Membresias/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Membresias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id_membresia,nombre_membresia,precio,vendidas")] Membresia membresia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(membresia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(membresia);
        }

        // GET: Membresias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Membresia == null)
            {
                return NotFound();
            }

            var membresia = await _context.Membresia.FindAsync(id);
            if (membresia == null)
            {
                return NotFound();
            }
            return View(membresia);
        }

        // POST: Membresias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id_membresia,nombre_membresia,precio,vendidas")] Membresia membresia)
        {
            if (id != membresia.id_membresia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(membresia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MembresiaExists(membresia.id_membresia))
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
            return View(membresia);
        }

        // GET: Membresias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Membresia == null)
            {
                return NotFound();
            }

            var membresia = await _context.Membresia
                .FirstOrDefaultAsync(m => m.id_membresia == id);
            if (membresia == null)
            {
                return NotFound();
            }

            return View(membresia);
        }

        // POST: Membresias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Membresia == null)
            {
                return Problem("Entity set 'gymDbContext.Membresia'  is null.");
            }
            var membresia = await _context.Membresia.FindAsync(id);
            if (membresia != null)
            {
                _context.Membresia.Remove(membresia);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MembresiaExists(int id)
        {
          return (_context.Membresia?.Any(e => e.id_membresia == id)).GetValueOrDefault();
        }
    }
}

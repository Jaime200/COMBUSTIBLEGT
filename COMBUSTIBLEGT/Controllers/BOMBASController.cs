using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DB;
using ENTIDADES;

namespace COMBUSTIBLEGT.Controllers
{
    public class BOMBASController : Controller
    {
        private readonly combustibleGTContext _context;

        public BOMBASController(combustibleGTContext context)
        {
            _context = context;
        }

        // GET: BOMBAS
        public async Task<IActionResult> Index()
        {
            return View(await _context.BOMBAS.ToListAsync());
        }

        // GET: BOMBAS/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bOMBA = await _context.BOMBAS
                .FirstOrDefaultAsync(m => m.ID_BOMBA == id);
            if (bOMBA == null)
            {
                return NotFound();
            }

            return View(bOMBA);
        }

        // GET: BOMBAS/Create
        public IActionResult Create()
        {
            getEstados();
            return View();
        }

        // POST: BOMBAS/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_BOMBA,DESCRIPCION,ESTADO")] BOMBA bOMBA)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bOMBA);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bOMBA);
        }

        // GET: BOMBAS/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            getEstados();
            var bOMBA = await _context.BOMBAS.FindAsync(id);
            if (bOMBA == null)
            {
                return NotFound();
            }
            return View(bOMBA);
        }

        // POST: BOMBAS/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_BOMBA,DESCRIPCION,ESTADO")] BOMBA bOMBA)
        {
            if (id != bOMBA.ID_BOMBA)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bOMBA);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BOMBAExists(bOMBA.ID_BOMBA))
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
            return View(bOMBA);
        }

        // GET: BOMBAS/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bOMBA = await _context.BOMBAS
                .FirstOrDefaultAsync(m => m.ID_BOMBA == id);
            if (bOMBA == null)
            {
                return NotFound();
            }

            return View(bOMBA);
        }

        // POST: BOMBAS/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bOMBA = await _context.BOMBAS.FindAsync(id);
            _context.BOMBAS.Remove(bOMBA);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BOMBAExists(int id)
        {
            return _context.BOMBAS.Any(e => e.ID_BOMBA == id);
        }

        public void getEstados()
        {
            List<SelectListItem> ESTADOS = new()
            {
                new SelectListItem { Value = "A", Text = "ACTIVO" },
                new SelectListItem { Value = "I", Text = "INACTIVO" }
            };
            ViewBag.ESTADOS = ESTADOS;
        }
    }
}

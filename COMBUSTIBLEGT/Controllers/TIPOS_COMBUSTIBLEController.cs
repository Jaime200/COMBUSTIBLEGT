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
    public class TIPOS_COMBUSTIBLEController : Controller
    {
        private readonly combustibleGTContext _context;

        public TIPOS_COMBUSTIBLEController(combustibleGTContext context)
        {
            _context = context;
        }

        // GET: TIPOS_COMBUSTIBLE
        public async Task<IActionResult> Index()
        {
         
            return View(await _context.TIPOS_COMBUSTIBLE.ToListAsync());
        }

        // GET: TIPOS_COMBUSTIBLE/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tIPO_COMBUSTIBLE = await _context.TIPOS_COMBUSTIBLE
                .FirstOrDefaultAsync(m => m.ID_TIPO_COMBUSTIBLE == id);
            if (tIPO_COMBUSTIBLE == null)
            {
                return NotFound();
            }

            return View(tIPO_COMBUSTIBLE);
        }

        // GET: TIPOS_COMBUSTIBLE/Create
        public IActionResult Create()
        {
            getEstados();
            return View();
        }

        // POST: TIPOS_COMBUSTIBLE/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_TIPO_COMBUSTIBLE,DESCRIPCION,ESTADO")] TIPO_COMBUSTIBLE tIPO_COMBUSTIBLE)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tIPO_COMBUSTIBLE);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tIPO_COMBUSTIBLE);
        }

        // GET: TIPOS_COMBUSTIBLE/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            getEstados();
            var tIPO_COMBUSTIBLE = await _context.TIPOS_COMBUSTIBLE.FindAsync(id);
            if (tIPO_COMBUSTIBLE == null)
            {
                return NotFound();
            }
            return View(tIPO_COMBUSTIBLE);
        }

        // POST: TIPOS_COMBUSTIBLE/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID_TIPO_COMBUSTIBLE,DESCRIPCION,ESTADO")] TIPO_COMBUSTIBLE tIPO_COMBUSTIBLE)
        {
            if (id != tIPO_COMBUSTIBLE.ID_TIPO_COMBUSTIBLE)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tIPO_COMBUSTIBLE);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TIPO_COMBUSTIBLEExists(tIPO_COMBUSTIBLE.ID_TIPO_COMBUSTIBLE))
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
            return View(tIPO_COMBUSTIBLE);
        }

        // GET: TIPOS_COMBUSTIBLE/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tIPO_COMBUSTIBLE = await _context.TIPOS_COMBUSTIBLE
                .FirstOrDefaultAsync(m => m.ID_TIPO_COMBUSTIBLE == id);
            if (tIPO_COMBUSTIBLE == null)
            {
                return NotFound();
            }

            return View(tIPO_COMBUSTIBLE);
        }

        // POST: TIPOS_COMBUSTIBLE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tIPO_COMBUSTIBLE = await _context.TIPOS_COMBUSTIBLE.FindAsync(id);
            _context.TIPOS_COMBUSTIBLE.Remove(tIPO_COMBUSTIBLE);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TIPO_COMBUSTIBLEExists(int id)
        {
            return _context.TIPOS_COMBUSTIBLE.Any(e => e.ID_TIPO_COMBUSTIBLE == id);
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

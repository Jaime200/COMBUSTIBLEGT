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
    public class VEHICULOSController : Controller
    {
        private readonly combustibleGTContext _context;

        public VEHICULOSController(combustibleGTContext context)
        {
            _context = context;
        }

        // GET: VEHICULOes
        public async Task<IActionResult> Index()
        {
            return View(await _context.VEHICULO.ToListAsync());
        }

        // GET: VEHICULOes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vEHICULO = await _context.VEHICULO
                .FirstOrDefaultAsync(m => m.PLACA == id);
            if (vEHICULO == null)
            {
                return NotFound();
            }

            return View(vEHICULO);
        }

        // GET: VEHICULOes/Create
        public IActionResult Create()
        {
            getEstados();
            return View();
        }

        // POST: VEHICULOes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PLACA,ANIO,MODELO,ESTADO")] VEHICULO vEHICULO)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vEHICULO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vEHICULO);
        }

        // GET: VEHICULOes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            getEstados();
            var vEHICULO = await _context.VEHICULO.FindAsync(id);
            if (vEHICULO == null)
            {
                return NotFound();
            }
            return View(vEHICULO);
        }

        // POST: VEHICULOes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PLACA,ANIO,MODELO,ESTADO")] VEHICULO vEHICULO)
        {
            if (id != vEHICULO.PLACA)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vEHICULO);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VEHICULOExists(vEHICULO.PLACA))
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
            return View(vEHICULO);
        }

        // GET: VEHICULOes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vEHICULO = await _context.VEHICULO
                .FirstOrDefaultAsync(m => m.PLACA == id);
            if (vEHICULO == null)
            {
                return NotFound();
            }

            return View(vEHICULO);
        }

        // POST: VEHICULOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var vEHICULO = await _context.VEHICULO.FindAsync(id);
            _context.VEHICULO.Remove(vEHICULO);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VEHICULOExists(string id)
        {
            return _context.VEHICULO.Any(e => e.PLACA == id);
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

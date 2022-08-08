using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DB;
using ENTIDADES;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Reporting.NETCore;

namespace COMBUSTIBLEGT.Controllers
{
    public class DESPACHOController : Controller
    {
        private readonly combustibleGTContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;
        public DESPACHOController(combustibleGTContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: DESPACHO
        public async Task<IActionResult> Index()
        {
            var combustibleGTContext = _context.DESPACHOS.Include(d => d.BOMBA).Include(d => d.TIPO_COMBUSTIBLE).Include(d => d.VEHICULO);
            return View(await combustibleGTContext.ToListAsync());
        }

           public async Task<IActionResult> Reporte()
        {
            
            List<SelectListItem> firstResult = new()
            {
                new SelectListItem { Value = "-1", Text = "--SELECCIONES UNA OPCION--" },
                
            };
            List<SelectListItem> resultQuery = _context.BOMBAS.Where(w => w.ESTADO == "A").Select(s=> new SelectListItem { Value = s.ID_BOMBA.ToString(), Text = s.DESCRIPCION }).ToList();
            var data  = firstResult.Union(resultQuery);
            ViewData["ID_BOMBA"] = data;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Reporte([Bind("FECHA_DESPACHO,ID_BOMBA")] ENTIDADES.REPORTES.REPORTE_DESPACHOS rEPORTE_DESPACHO)
        {
            try
            {

            
            ViewData["ID_BOMBA"] = new SelectList(_context.BOMBAS.Where(w => w.ESTADO == "A"), "ID_BOMBA", "DESCRIPCION");
            var path = $"{_webHostEnviroment.WebRootPath}\\Reportes\\Report.rdlc";

            IQueryable <DESPACHO> query =  _context.DESPACHOS;
            if(rEPORTE_DESPACHO.ID_BOMBA >= 0)
            {
                query = query.Where(W => W.ID_BOMBA == rEPORTE_DESPACHO.ID_BOMBA);
            }
            if(rEPORTE_DESPACHO.FECHA_DESPACHO != null)
            {
                
                var DbF = Microsoft.EntityFrameworkCore.EF.Functions;
                
                string strFecha = rEPORTE_DESPACHO.FECHA_DESPACHO.Value.ToString("dd/MM/yyyy");
                query = query.Where(W =>
                 W.FECHA_DESPACHO <= rEPORTE_DESPACHO.FECHA_DESPACHO
                
                );
            }
            var resultquery = await query.Select(s => new
            {
                BOMBA = s.BOMBA.DESCRIPCION,
                PRECIO = s.PRECIO_GALON,
                CANTIDAD = s.CANTIDAD_GALONES,
                TOTAL = (s.PRECIO_GALON * s.CANTIDAD_GALONES),
                FECHA = s.FECHA_DESPACHO
            }).ToListAsync();
            string renderFormat = "PDF";
            string extension = "pdf";
            string mimetype = "application/pdf";

            using var report = new LocalReport();
            report.ReportPath = path;
            report.DataSources.Add(new ReportDataSource("DtsReporte", resultquery));
            var pdf = report.Render(renderFormat);
            return File(pdf, mimetype, $"Reporte{DateTime.Now}.{extension}");
            }
            catch (Exception ex )
            {

                throw ex;
            }

        }

        // GET: DESPACHO/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dESPACHO = await _context.DESPACHOS
                .Include(d => d.BOMBA)
                .Include(d => d.TIPO_COMBUSTIBLE)
                .Include(d => d.VEHICULO)
                .FirstOrDefaultAsync(m => m.ID_DESPACHO == id);
            if (dESPACHO == null)
            {
                return NotFound();
            }

            return View(dESPACHO);
        }

        // GET: DESPACHO/Create
        public IActionResult Create()
        {
            ViewData["ID_BOMBA"] = new SelectList(_context.BOMBAS.Where(w=>w.ESTADO =="A"), "ID_BOMBA", "DESCRIPCION");
            ViewData["ID_TIPO_COMBUSTIBLE"] = new SelectList(_context.TIPOS_COMBUSTIBLE.Where(w => w.ESTADO == "A"), "ID_TIPO_COMBUSTIBLE", "DESCRIPCION");
            ViewData["PLACA"] = new SelectList(_context.VEHICULO.Where(w => w.ESTADO == "A"), "PLACA", "PLACA");
            return View();
        }

        // POST: DESPACHO/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID_DESPACHO,PLACA,ID_BOMBA,ID_TIPO_COMBUSTIBLE,CANTIDAD_GALONES,PRECIO_GALON,FECHA_DESPACHO")] DESPACHO dESPACHO)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dESPACHO);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Create));
            }
            ViewData["ID_BOMBA"] = new SelectList(_context.BOMBAS, "ID_BOMBA", "DESCRIPCION", dESPACHO.ID_BOMBA);
            ViewData["ID_TIPO_COMBUSTIBLE"] = new SelectList(_context.TIPOS_COMBUSTIBLE, "ID_TIPO_COMBUSTIBLE", "DESCRIPCION", dESPACHO.ID_TIPO_COMBUSTIBLE);
            ViewData["PLACA"] = new SelectList(_context.VEHICULO, "PLACA", "PLACA", dESPACHO.PLACA);
            return View(dESPACHO);
        }

        // GET: DESPACHO/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var dESPACHO = await _context.DESPACHOS.FindAsync(id);
        //    if (dESPACHO == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["ID_BOMBA"] = new SelectList(_context.BOMBAS, "ID_BOMBA", "DESCRIPCION", dESPACHO.ID_BOMBA);
        //    ViewData["ID_TIPO_COMBUSTIBLE"] = new SelectList(_context.TIPOS_COMBUSTIBLE, "ID_TIPO_COMBUSTIBLE", "DESCRIPCION", dESPACHO.ID_TIPO_COMBUSTIBLE);
        //    ViewData["PLACA"] = new SelectList(_context.VEHICULO, "PLACA", "PLACA", dESPACHO.PLACA);
        //    return View(dESPACHO);
        //}

        // POST: DESPACHO/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("ID_DESPACHO,PLACA,ID_BOMBA,ID_TIPO_COMBUSTIBLE,CANTIDAD_GALONES,PRECIO_GALON,FECHA_DESPACHO")] DESPACHO dESPACHO)
        //{
        //    if (id != dESPACHO.ID_DESPACHO)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(dESPACHO);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!DESPACHOExists(dESPACHO.ID_DESPACHO))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["ID_BOMBA"] = new SelectList(_context.BOMBAS, "ID_BOMBA", "DESCRIPCION", dESPACHO.ID_BOMBA);
        //    ViewData["ID_TIPO_COMBUSTIBLE"] = new SelectList(_context.TIPOS_COMBUSTIBLE, "ID_TIPO_COMBUSTIBLE", "DESCRIPCION", dESPACHO.ID_TIPO_COMBUSTIBLE);
        //    ViewData["PLACA"] = new SelectList(_context.VEHICULO, "PLACA", "PLACA", dESPACHO.PLACA);
        //    return View(dESPACHO);
        //}

        // GET: DESPACHO/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dESPACHO = await _context.DESPACHOS
                .Include(d => d.BOMBA)
                .Include(d => d.TIPO_COMBUSTIBLE)
                .Include(d => d.VEHICULO)
                .FirstOrDefaultAsync(m => m.ID_DESPACHO == id);
            if (dESPACHO == null)
            {
                return NotFound();
            }

            return View(dESPACHO);
        }

        // POST: DESPACHO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dESPACHO = await _context.DESPACHOS.FindAsync(id);
            _context.DESPACHOS.Remove(dESPACHO);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DESPACHOExists(int id)
        {
            return _context.DESPACHOS.Any(e => e.ID_DESPACHO == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GlobalSolution.Data;
using GlobalSolution.Entity;

namespace GlobalSolution.Controllers
{
    public class ContatoEmergenciasController : Controller
    {
        private readonly GlobalSolutionContext _context;

        public ContatoEmergenciasController(GlobalSolutionContext context)
        {
            _context = context;
        }

        // GET: ContatoEmergencias
        public async Task<IActionResult> Index()
        {
            var globalSolutionContext = _context.ContatoEmergencia.Include(c => c.Paciente);
            return View(await globalSolutionContext.ToListAsync());
        }

        // GET: ContatoEmergencias/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.ContatoEmergencia == null)
            {
                return NotFound();
            }

            var contatoEmergencia = await _context.ContatoEmergencia
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contatoEmergencia == null)
            {
                return NotFound();
            }

            return View(contatoEmergencia);
        }

        // GET: ContatoEmergencias/Create
        public IActionResult Create(Paciente paciente)
        {
            ViewData["IdPaciente"] = new SelectList(_context.Paciente.Include(p => p.Usuario), "Id", "Usuario.Nome");
            return View();
        }

        // POST: ContatoEmergencias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Telefone,IdPaciente")] ContatoEmergencia contatoEmergencia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contatoEmergencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPaciente"] = new SelectList(_context.Paciente, "Id", "Colesterol", contatoEmergencia.IdPaciente);
            return View(contatoEmergencia);
        }

        // GET: ContatoEmergencias/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.ContatoEmergencia == null)
            {
                return NotFound();
            }

            var contatoEmergencia = await _context.ContatoEmergencia.FindAsync(id);
            if (contatoEmergencia == null)
            {
                return NotFound();
            }
            ViewData["IdPaciente"] = new SelectList(_context.Paciente, "Id", "Colesterol", contatoEmergencia.IdPaciente);
            return View(contatoEmergencia);
        }

        // POST: ContatoEmergencias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nome,Telefone,IdPaciente")] ContatoEmergencia contatoEmergencia)
        {
            if (id != contatoEmergencia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contatoEmergencia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContatoEmergenciaExists(contatoEmergencia.Id))
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
            ViewData["IdPaciente"] = new SelectList(_context.Paciente, "Id", "Colesterol", contatoEmergencia.IdPaciente);
            return View(contatoEmergencia);
        }

        // GET: ContatoEmergencias/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.ContatoEmergencia == null)
            {
                return NotFound();
            }

            var contatoEmergencia = await _context.ContatoEmergencia
                .Include(c => c.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contatoEmergencia == null)
            {
                return NotFound();
            }

            return View(contatoEmergencia);
        }

        // POST: ContatoEmergencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.ContatoEmergencia == null)
            {
                return Problem("Entity set 'GlobalSolutionContext.ContatoEmergencia'  is null.");
            }
            var contatoEmergencia = await _context.ContatoEmergencia.FindAsync(id);
            if (contatoEmergencia != null)
            {
                _context.ContatoEmergencia.Remove(contatoEmergencia);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContatoEmergenciaExists(long id)
        {
          return (_context.ContatoEmergencia?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

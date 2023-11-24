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
    public class PacientesController : Controller
    {
        private readonly GlobalSolutionContext _context;
        private static string? _usuarioEmail { get; set; }
        private static long? _usuarioId { get; set; }

        public PacientesController(GlobalSolutionContext context)
        {
            _context = context;
        }

        // GET: Pacientes
        public async Task<IActionResult> Index()
        {
            var globalSolutionContext = _context.Paciente.Include(p => p.Usuario);
            return View(await globalSolutionContext.ToListAsync());
        }

        // GET: Pacientes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Paciente == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // GET: Pacientes/Create
        public IActionResult Create(Usuario usuario)
        {
            ViewBag.UsuarioId = usuario.Id;
            _usuarioId = usuario.Id;
            ViewBag.UsuarioEmail = usuario.Email;
            _usuarioEmail = usuario.Email;

            return View();
        }

        // POST: Pacientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UsuarioId,Idade,SexoBiologico,Genero,Colesterol,Triglicerol,Diabete,Fumante,Obeso,ConsumoAlcool,Dieta,UsoMedicamentos")] Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paciente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            Console.WriteLine(string.Join("; ", ModelState.Values
                            .SelectMany(x => x.Errors)
                            .Select(x => x.ErrorMessage)));

            ViewBag.UsuarioEmail = _usuarioEmail;
            ViewBag.UsuarioId = _usuarioId;
            return View(paciente);
        }

        // GET: Pacientes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Paciente == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente.FindAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuario.FindAsync(paciente.UsuarioId);
            ViewBag.UsuarioId = paciente.UsuarioId;
            _usuarioId = usuario.Id;
            ViewBag.UsuarioEmail = usuario.Email;
            _usuarioEmail = usuario.Email;
            return View(paciente);
        }

        // POST: Pacientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,UsuarioId,Idade,SexoBiologico,Genero,Colesterol,Triglicerol,Diabete,Fumante,Obeso,ConsumoAlcool,Dieta,UsoMedicamentos")] Paciente paciente)
        {
            if (id != paciente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paciente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteExists(paciente.Id))
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
            ViewBag.UsuarioEmail = _usuarioEmail;
            ViewBag.UsuarioId = _usuarioId;
            return View(paciente);
        }

        // GET: Pacientes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Paciente == null)
            {
                return NotFound();
            }

            var paciente = await _context.Paciente
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (paciente == null)
            {
                return NotFound();
            }

            return View(paciente);
        }

        // POST: Pacientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Paciente == null)
            {
                return Problem("Entity set 'GlobalSolutionContext.Paciente'  is null.");
            }
            var paciente = await _context.Paciente.FindAsync(id);
            if (paciente != null)
            {
                _context.Paciente.Remove(paciente);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteExists(long id)
        {
          return (_context.Paciente?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

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
    public class MonitorsController : Controller
    {
        private readonly GlobalSolutionContext _context;
        private static string? _usuarioEmail { get; set; }

        public MonitorsController(GlobalSolutionContext context)
        {
            _context = context;
        }

        // GET: Monitors
        public async Task<IActionResult> Index()
        {
            var globalSolutionContext = _context.Monitor
                .Include(m => m.Instituicao)
                .Include(m => m.Usuario)
                .Include(m => m.MonitorPacientes)
                    .ThenInclude(mp => mp.Paciente)
                    .ThenInclude(p => p.Usuario);

            return View(await globalSolutionContext.ToListAsync());
        }

        // GET: Monitors/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Monitor == null)
            {
                return NotFound();
            }

            var monitor = await _context.Monitor
                .Include(m => m.Instituicao)
                .Include(m => m.Usuario)
                .Include(m => m.MonitorPacientes)
                    .ThenInclude(mp => mp.Paciente)
                    .ThenInclude(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (monitor == null)
            {
                return NotFound();
            }

            return View(monitor);
        }

        // GET: Monitors/Create
        public IActionResult Create(Usuario usuario)
        {
            ViewBag.UsuarioId = usuario.Id;
            ViewBag.UsuarioEmail = usuario.Email;
            _usuarioEmail = usuario.Email;
            ViewData["Pacientes"] = new SelectList(_context.Paciente
                .Include(p => p.Usuario)
                .ToList(), "Id", "Usuario.Nome");

            return View();
        }

        // POST: Monitors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdUsuario,PacienteId")] Entity.Monitor monitor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(monitor);
                await _context.SaveChangesAsync();
                foreach (var paciente in monitor.PacienteId)
                { 
                    var monitorPaciente = new MonitorPaciente
                    {
                        MonitorId = monitor.Id,
                        PacienteId = paciente,
                        Paciente = _context.Paciente.Find(paciente),
                        Monitor = _context.Monitor.Find(monitor.Id)
                    };
                    Console.WriteLine(monitorPaciente.MonitorId);
                    Console.WriteLine(monitorPaciente.Monitor);
                    Console.WriteLine(monitorPaciente.PacienteId);
                    Console.WriteLine(monitorPaciente.Paciente);
                    _context.Add(monitorPaciente);

                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            Console.WriteLine(string.Join("; ", ModelState.Values
                            .SelectMany(x => x.Errors)
                            .Select(x => x.ErrorMessage)));
            ViewBag.UsuarioEmail = _usuarioEmail;
            ViewBag.UsuarioId = monitor.IdUsuario;
            ViewData["Pacientes"] = new SelectList(_context.Paciente
                .Include(p => p.Usuario)
                .ToList(), "Id", "Usuario.Nome");
            return View(monitor);
        }

        // GET: Monitors/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Monitor == null)
            {
                return NotFound();
            }

            var monitor = await _context.Monitor
                .Include(m => m.MonitorPacientes)
                    .ThenInclude(mp => mp.Paciente)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (monitor == null)
            {
                return NotFound();
            }

            ViewBag.ExisteInstituicao = _context.Instituicao.Any();
            ViewBag.UsuarioId = monitor.IdUsuario;
            ViewBag.UsuarioEmail = _context.Usuario.Find(monitor.IdUsuario).Email;

            ViewData["IdInstituicao"] = new SelectList(_context.Instituicao, "Id", "Endereco", monitor.IdInstituicao);
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "Id", "Email", monitor.IdUsuario);
            ViewData["Pacientes"] = new SelectList(_context.Paciente
                .Include(p => p.Usuario)
                .ToList(), "Id", "Usuario.Nome", monitor.MonitorPacientes.Select(mp => mp.Paciente.Id));

            return View(monitor);
        }

        // POST: Monitors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,IdUsuario,IdInstituicao,PacienteId")] Entity.Monitor monitor)
        {
            if (id != monitor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(monitor);

                    // Remova os MonitorPacientes existentes para evitar duplicatas
                    _context.MonitorPaciente.RemoveRange(_context.MonitorPaciente.Where(mp => mp.MonitorId == monitor.Id));

                    // Adicione os MonitorPacientes atualizados
                    foreach (var pacienteId in monitor.PacienteId)
                    {
                        var monitorPaciente = new MonitorPaciente
                        {
                            MonitorId = monitor.Id,
                            PacienteId = pacienteId,
                        };
                        _context.Add(monitorPaciente);
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MonitorExists(monitor.Id))
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

            ViewData["IdInstituicao"] = new SelectList(_context.Instituicao, "Id", "Endereco", monitor.IdInstituicao);
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "Id", "Email", monitor.IdUsuario);
            ViewData["Pacientes"] = new SelectList(_context.Paciente
                .Include(p => p.Usuario)
                .ToList(), "Id", "Usuario.Nome", monitor.MonitorPacientes.Select(mp => mp.Paciente.Id));

            return View(monitor);
        }

        // GET: Monitors/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Monitor == null)
            {
                return NotFound();
            }

            var monitor = await _context.Monitor
                .Include(m => m.Instituicao)
                .Include(m => m.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (monitor == null)
            {
                return NotFound();
            }

            return View(monitor);
        }

        // POST: Monitors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Monitor == null)
            {
                return Problem("Entity set 'GlobalSolutionContext.Monitor'  is null.");
            }
            var monitor = await _context.Monitor.FindAsync(id);
            if (monitor != null)
            {
                _context.Monitor.Remove(monitor);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MonitorExists(long id)
        {
          return (_context.Monitor?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

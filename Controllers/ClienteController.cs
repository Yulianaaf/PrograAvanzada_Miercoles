using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPymeStore.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace MyPymeStore.Controllers
{
    public class ClientesController : Controller
    {
        private readonly MyPymeStoreContext _context;

        public ClientesController(MyPymeStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View();

        // Gestión de clientes con búsqueda
        public async Task<IActionResult> GestionClientes(string searchName, string searchCedula)
        {
            var query = _context.Clientes.AsQueryable();

            if (!string.IsNullOrEmpty(searchName))
                query = query.Where(c => c.Nombre.Contains(searchName));

            if (!string.IsNullOrEmpty(searchCedula))
                query = query.Where(c => c.Cedula.Contains(searchCedula));

            var clientes = await query.OrderBy(c => c.Id).ToListAsync();

            ViewBag.CurrentName = searchName;
            ViewBag.CurrentCedula = searchCedula;

            return View(clientes);
        }

        // Crear cliente
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente cliente)
        {
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(cliente.Nombre))
                ModelState.AddModelError("Nombre", "El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(cliente.Cedula))
                ModelState.AddModelError("Cedula", "La cédula es obligatoria.");

            if (string.IsNullOrWhiteSpace(cliente.Correo) || !EsCorreoValido(cliente.Correo))
                ModelState.AddModelError("Correo", "Debe ingresar un correo válido.");

            if (string.IsNullOrWhiteSpace(cliente.Telefono))
                ModelState.AddModelError("Telefono", "El teléfono es obligatorio.");

            if (string.IsNullOrWhiteSpace(cliente.Direccion))
                ModelState.AddModelError("Direccion", "La dirección es obligatoria.");

            if (ModelState.IsValid)
            {
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GestionClientes));
            }

            return View(cliente);
        }

        // Editar cliente
        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Cliente cliente)
        {
            if (!ModelState.IsValid)
                return View(cliente);

            var clienteDb = await _context.Clientes.FindAsync(cliente.Id);
            if (clienteDb == null)
                return NotFound();

            clienteDb.Nombre = cliente.Nombre;
            clienteDb.Cedula = cliente.Cedula;
            clienteDb.Correo = cliente.Correo;
            clienteDb.Telefono = cliente.Telefono;
            clienteDb.Direccion = cliente.Direccion;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(GestionClientes));
        }

        // Detalles del cliente
        public async Task<IActionResult> Details(int id)
        {
            var cliente = await _context.Clientes.FirstOrDefaultAsync(c => c.Id == id);
            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        // Eliminar cliente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(GestionClientes));
        }

        // Validación de correo
        private bool EsCorreoValido(string correo)
        {
            return Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}



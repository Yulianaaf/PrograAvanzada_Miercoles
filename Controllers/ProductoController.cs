using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPymeStore.Models;
using System.Threading.Tasks;

namespace MyPymeStore.Controllers
{
    public class ProductosController : Controller
    {
        private readonly MyPymeStoreContext _context;

        public ProductosController(MyPymeStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GestionProductos()
        {
            ViewBag.Categorias = await _context.Categorias.ToListAsync();
            var productos = await _context.Productos
                .Include(p => p.Categoria)
                .ToListAsync();
            return View(productos);
        }

        public IActionResult Create()
        {
            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GestionProductos));
            }

            ViewBag.Categorias = new SelectList(_context.Categorias, "Id", "Nombre");
            return View(producto);
        }

        public async Task<IActionResult> Details(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null) return NotFound();

            return View(producto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Update(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GestionProductos));
            }

            ViewBag.Categorias = await _context.Categorias.ToListAsync();
            var productos = await _context.Productos.Include(p => p.Categoria).ToListAsync();
            return View("GestionProductos", productos);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(GestionProductos));
        }
    }
}

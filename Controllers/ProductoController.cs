using Microsoft.AspNetCore.Mvc;
using MyPymeStore.Models;

using Microsoft.AspNetCore.Mvc;
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

        // Página principal (menú)
        public IActionResult Index()
        {
            return View();
        }

        // Página para listar productos
        public async Task<IActionResult> GestionProductos()
        {
            var productos = await _context.Productos.Include(p => p.Categoria).ToListAsync();
            return View(productos);
        }

        // Crear producto
        public IActionResult Create()
        {
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
            return View(producto);
        }

        // Ver detalles
        public async Task<IActionResult> Details(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.IdProducto == id);

            if (producto == null) return NotFound();

            return View(producto);
        }
    }
}

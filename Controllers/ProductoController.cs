using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyPymeStore.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MyPymeStore.Controllers
{
    public class ProductosController : Controller
    {
        private readonly MyPymeStoreContext _context;
        private const int PageSize = 5;

        public ProductosController(MyPymeStoreContext context)
        {
            _context = context;
        }

        public IActionResult Index() => View();

        // Gestión con filtros y paginación
        public async Task<IActionResult> GestionProductos(string searchName, int? categoryId, int page = 1)
        {
            var query = _context.Productos.Include(p => p.Categoria).AsQueryable();

            if (!string.IsNullOrEmpty(searchName))
                query = query.Where(p => p.Nombre.Contains(searchName));

            if (categoryId.HasValue && categoryId.Value > 0)
                query = query.Where(p => p.CategoriaId == categoryId.Value);

            var totalItems = await query.CountAsync();
            var productos = await query
                .OrderBy(p => p.Id)
                .Skip((page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();

            ViewBag.CategoriasSelectList = await _context.Categorias
                .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nombre })
                .ToListAsync();

            ViewBag.CurrentName = searchName;
            ViewBag.CurrentCategory = categoryId;
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

            return View(productos);
        }

        // Crear producto
        public IActionResult Create()
        {
            ViewBag.CategoriasSelectList = new SelectList(_context.Categorias, "Id", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Producto producto)
        {
            // Si no se ingresó IVA, se calcula 13%
            if (producto.ImpuestosPorCompra <= 0)
                producto.ImpuestosPorCompra = producto.Precio * 0.13m;

            if (ModelState.IsValid)
            {
                _context.Productos.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GestionProductos));
            }

            ViewBag.CategoriasSelectList = new SelectList(_context.Categorias, "Id", "Nombre", producto.CategoriaId);
            return View(producto);
        }

        // Editar producto desde modal
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Producto producto)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(GestionProductos));

            var productoDb = await _context.Productos.FindAsync(producto.Id);
            if (productoDb == null)
                return NotFound();

            // Actualizar campos con datos del modal, incluyendo IVA
            productoDb.Nombre = producto.Nombre;
            productoDb.Precio = producto.Precio;
            productoDb.ImpuestosPorCompra = producto.ImpuestosPorCompra;
            productoDb.Stock = producto.Stock;
            productoDb.CategoriaId = producto.CategoriaId;
            productoDb.Activo = producto.Activo;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(GestionProductos));
        }

        // Detalles del producto
        public async Task<IActionResult> Details(int id)
        {
            var producto = await _context.Productos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (producto == null) return NotFound();

            return View(producto);
        }

        // Eliminar producto
        [HttpPost]
        [ValidateAntiForgeryToken]
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

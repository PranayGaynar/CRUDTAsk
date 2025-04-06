using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskComp.ContextFile;
using TaskComp.Models;
using X.PagedList.Extensions;


namespace TaskComp.Controllers
{
    public class ProductController : Controller
    {
        private readonly MyNewDbContext _context;
        public ProductController(MyNewDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(int page = 1)
        {
            int pageSize = 10;  // Number of products per page
            var products = _context.Product
                                    .Include(p => p.categorymasters)  // Assuming you have a navigation property for Category
                                    .ToList()
                                    .ToPagedList(page, pageSize);

            return View(products);
        }


        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.categorymasters)  // Ensure the category is included
                .FirstOrDefaultAsync(s => s.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }



        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductMaster product)
        {
            if (!ModelState.IsValid)
            {
                await _context.Product.AddAsync(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductMaster product)
        {
            if (id != product.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                        return NotFound();

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.category.FindAsync(id);
            _context.category.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }




        private bool ProductExists(int id)
        {
            return _context.Product.Any(s=>s.Id == id);
        }

        //  Hii Pranay


    }
}

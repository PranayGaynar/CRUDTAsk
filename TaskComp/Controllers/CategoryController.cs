using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskComp.ContextFile;
using TaskComp.Models;

namespace TaskComp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly MyNewDbContext _context;
        public CategoryController(MyNewDbContext context ) {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.category.ToListAsync());
        }

        [HttpGet]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var category = await _context.category
                .Include(c => c.productmasters)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryMaster category)
        {

            _context.Add(category);
                await _context.SaveChangesAsync();
            return View(category);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var category = await _context.category.FindAsync(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CategoryMaster category)
        {
            if (id != category.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryMasterExists(category.Id))
                        return NotFound();

                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var category = await _context.category
                .FirstOrDefaultAsync(m => m.Id == id);

            if (category == null)
                return NotFound();

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.category.FindAsync(id);
            _context.category.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }








        private bool CategoryMasterExists(int id)
        {
            return _context.category.Any(e => e.Id == id);
        }
    }
}

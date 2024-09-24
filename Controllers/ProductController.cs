using InvManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InvManagement.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDBcontext _context;
        private readonly DbSet<Product> _dbSet;

        public ProductController(ApplicationDBcontext context)
        {
            _context = context;
            _dbSet = _context.Set<Product>();
        }

        public IActionResult GetAllProducts()
        {

            var products = _dbSet.ToList();
            return View(products);
        }
        public IActionResult GetProduct(int id)
        {
            var product = _dbSet.Find(id);
            return View(product);
        }
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                Add(product);
                return RedirectToAction("Index");
            }
            return View("Index");
        }


        public void Add(Product entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void update(Product entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public IActionResult Update(int id, Product product)
        {
            if (product == null)
            {
                return NotFound();
            }

            product = _dbSet.Find(id);
            if (id != product.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                update(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        public void delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }



        public IActionResult Delete(int id)
        {
            var product = _dbSet.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                delete(product.ID);
            }
            return View(product);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}

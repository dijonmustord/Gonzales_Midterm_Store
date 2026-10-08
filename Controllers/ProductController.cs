using Microsoft.AspNetCore.Mvc;

using Gonzales_Midterm_Store.Data;

using FirstProduct.Models;
using FirstCart.Models;



namespace firstASP.Controllers

{

    public class ProductsController : Controller

    {

        private readonly ApplicationDbContext _db;

        public ProductsController(ApplicationDbContext db) { _db = db; }

 

         public IActionResult Index(string searchString)
             {
             var product = _db.Products.AsQueryable();

                if (!string.IsNullOrEmpty(searchString))
           {
            product = product.Where(p => p.Name.ToLower().Contains(searchString.ToLower()));
           }
                ViewData["searchString"] = searchString;
                 return View(product.ToList());

            }

        // shows the list


 

        // shows the empty add-form

        public IActionResult Create()

        {

            return View();

        }

 

        // saves a new product

        [HttpPost]

        public IActionResult Create(Products product)

        {

            _db.Products.Add(product);

            _db.SaveChanges();

            return RedirectToAction("Index");

        }

           // EDIT - show the edit form
        public IActionResult Edit(int id)
        {
            var product = _db.Products.Find(id);
            if (product == null) return RedirectToAction("Index");
            return View(product);
        }

        // EDIT - save the changes
        [HttpPost]
        public IActionResult Edit(Products product)
        {
            _db.Products.Update(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        // DELETE - remove the product
        public IActionResult Delete(int id)
        {
            var product = _db.Products.Find(id);
            if (product != null)
            {
                _db.Products.Remove(product);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        
        

    }

}
using Microsoft.AspNetCore.Mvc;
using Gonzales_Midterm_Store.Data;
using FirstCart.Models;

namespace firstASP.Controllers

{

    public class CartController : Controller

    {

        private readonly ApplicationDbContext _db;

        public CartController(ApplicationDbContext db) { _db = db; }

        public IActionResult Index()
         {

         var cartItems = _db.Cart.ToList();
         return View(cartItems);
        }

    
        public IActionResult AddToCart(int id)
        {
   
            var product = _db.Products.Find(id);
             if (product == null)
             {
               return NotFound();
            }

         var cartShop = _db.Cart.FirstOrDefault(c => c.ProductId == product.Id);

         if (cartShop != null)
         {
            cartShop.Quantity += 1;
           _db.Cart.Update(cartShop);
         }
         else
         {
       
               var scartShop = new Cart
             {
               ProductId = product.Id,
               ProductName = product.Name,
               Price = product.Price,
               Quantity = 1
          };
          _db.Cart.Add(scartShop);
             }

           _db.SaveChanges();

            return RedirectToAction("Index", "Products");
            }

            [HttpPost]
        public IActionResult UpdateQuantity(int id, int quantity)
        {
            var cartShop = _db.Cart.Find(id);
            if (cartShop != null)
            {
                if (quantity > 0)
                {
                    cartShop.Quantity = quantity;
                    _db.Cart.Update(cartShop);
                }
                else
                {
                    _db.Cart.Remove(cartShop);
                }
                _db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

            public IActionResult Remove(int id)
        {
            var cartShop = _db.Cart.Find(id);
            if (cartShop != null)
            {
                _db.Cart.Remove(cartShop);
                _db.SaveChanges();
            }
            return RedirectToAction("Index");

         }
  

    }

}
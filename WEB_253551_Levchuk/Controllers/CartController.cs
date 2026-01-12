using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WEB_253551_Levchuk.Models;

namespace WEB_253551_Levchuk.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        // Временное хранилище корзины (в реальном приложении использовать Session или БД)
        private static List<CartItem> _cartItems = new List<CartItem>
        {
            new CartItem 
            { 
                ProductId = 1, 
                ProductName = "Товар 1", 
                Price = 100.00M, 
                Quantity = 2 
            },
            new CartItem 
            { 
                ProductId = 2, 
                ProductName = "Товар 2", 
                Price = 200.00M, 
                Quantity = 1 
            }
        };

        public IActionResult Index()
        {
            var total = _cartItems.Sum(item => item.Price * item.Quantity);
            ViewBag.Total = total;
            ViewBag.ItemCount = _cartItems.Sum(item => item.Quantity);
            
            return View(_cartItems);
        }

        [HttpPost]
        public IActionResult AddToCart(int productId, string productName, decimal price)
        {
            var existingItem = _cartItems.FirstOrDefault(i => i.ProductId == productId);
            
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                _cartItems.Add(new CartItem
                {
                    ProductId = productId,
                    ProductName = productName,
                    Price = price,
                    Quantity = 1
                });
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var item = _cartItems.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                _cartItems.Remove(item);
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var item = _cartItems.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                if (quantity > 0)
                {
                    item.Quantity = quantity;
                }
                else
                {
                    _cartItems.Remove(item);
                }
            }
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            _cartItems.Clear();
            return RedirectToAction("Index");
        }
    }
}


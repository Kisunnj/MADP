using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using WEB_253551_Levchuk.Models;

namespace WEB_253551_Levchuk.Components
{
    public class CartComponent : ViewComponent
    {
        private List<Cart> _cart = new List<Cart> {
            new Cart{ Controller="Cart", Action="Index", Cost=0.00F,
                CostClassStyle="navbar-text ml-auto", Amount=2,
                AmountClassStyle="fas fa-shopping-cart nav-color", AmountStyle="margin-left: 10px;"
                }
        };
        public IViewComponentResult Invoke()
        {
            return View(_cart);
        }
    }
}

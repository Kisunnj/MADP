using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Razor.Compilation;
using WEB_253551_Levchuk.Models;

namespace WEB_253551_Levchuk.Components
{
    public class MenuComponent:ViewComponent
    {
        private List<MenuItem> _menuItems = new List<MenuItem>
        {
            new MenuItem{ Controller="Home", Action="Index", Text="Lab 2"},
            new MenuItem{ Controller="Product", Action="Index",
            Text="Каталог"},
            new MenuItem{ IsPage=true, Area="Admin", Page="/Index",
            Text="Администрирование"}
        };
        public IViewComponentResult Invoke()
        {
            foreach (MenuItem item in _menuItems)
            {
                if((ViewContext.RouteData.Values["controller"]?.ToString() != null && ViewContext.RouteData.Values["controller"]?.ToString() == item.Controller) ||
               (ViewContext.RouteData.Values["area"]?.ToString() != null && ViewContext.RouteData.Values["area"]?.ToString() == item.Controller) ||
                (ViewContext.RouteData.Values["page"]?.ToString() != null && ViewContext.RouteData.Values["page"]?.ToString() == item.Controller) )
                {
                    item.Active = "active-link";
                }
            }
            return View(_menuItems);
        }
    }
}

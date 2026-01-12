using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using WEB_253551_Levchuk.Models;

namespace WEB_253551_Levchuk.Controllers
{
    public class HomeController : Controller
    {
        private List<ListDemo> _listDemo;

        public HomeController()
        {
            _listDemo = new();
            _listDemo.Add(new ListDemo { ListItemValue = 1, ListItemText = "Item 1" });
            _listDemo.Add(new ListDemo { ListItemValue = 2, ListItemText = "Item 2" });
            _listDemo.Add(new ListDemo { ListItemValue = 3, ListItemText = "Item 3" });
        }

        public IActionResult Index()
        {
            ViewData["Text"] = "Лабораторная работа 2";
            ViewData["SelectedList"] = new SelectList(_listDemo, "ListItemValue", "ListItemText");
            return View();
        }

    }
    public class ListDemo
    {
        public int ListItemValue { get; set; }
        public string ListItemText { get; set; }
    }
}
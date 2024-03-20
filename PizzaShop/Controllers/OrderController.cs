using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Models;

namespace PizzaShop.Controllers
{
	public class OrderController : Controller
	{
		AppDBContext _context;
		public OrderController(AppDBContext context)
		{
			_context = context;
			ViewData["hasOrder"] = checkForOrder(_context);
		}
		public IActionResult Index()
		{
			ViewData["hasOrder"] = checkForOrder(_context);
			return View();
		}
		
		public IActionResult Show(int id) 
		{
			ViewData["hasOrder"] = checkForOrder(_context);
			Order order = _context.Orders.Include(o => o.Pizzas).ThenInclude(p => p.Toppings).ThenInclude(t => t.Topping).FirstOrDefault(o => o.Id == id);
			return View(order);
		}
		
		public IActionResult Create() 
		{
			Order order = new();
			order.UserId = 1;
			order.Price = 0;
			order.Status = 0;
			_context.Add(order);
			_context.SaveChanges();
			ViewData["hasOrder"] = true;
			return RedirectToAction("Show", new { id = order.Id });
		}
		
		public IActionResult GetOrderForShow() 
		{
			Order order = _context.Orders.Include(o => o.Pizzas).ThenInclude(p => p.Toppings).FirstOrDefault(o => o.Status == 0);
			return RedirectToAction("Show", new { id = order.Id });
		}
		
		static public bool checkForOrder(AppDBContext context) 
		{
			Order order = context.Orders.FirstOrDefault(o => o.Status == 0);
			if (order == null) 
			{
				return false;
			}
			return true;
		}
	}
}

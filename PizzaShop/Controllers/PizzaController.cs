using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaShop.Models;

namespace PizzaShop.Controllers
{
	public class PizzaController : Controller
	{
		private readonly AppDBContext context;
		public PizzaController(AppDBContext ctx)
		{
			context = ctx;
			ViewData["hasOrder"] = OrderController.checkForOrder(context);
		}
		public IActionResult Index()
		{
			ViewData["hasOrder"] = OrderController.checkForOrder(context);
			List<Pizza> pizzas = context.Pizzas.Include(p => p.Toppings).ThenInclude(t => t.Topping).ToList();
			return View(pizzas);
		}
		public IActionResult Create(int id)
		{
			ViewData["hasOrder"] = OrderController.checkForOrder(context);
			return View();
		}
		[HttpPost]
		public IActionResult Create(int id, IFormCollection form)
		{
			double price = 0;
			Pizza pizza = new Pizza
			{
				Size = Convert.ToInt32(form["Size"]),
				Crust = Convert.ToInt32(form["Crust"]),
				Price = price,
				OrderId = id
			};
			context.Pizzas.Add(pizza);
			context.SaveChanges();
			foreach(var topping in form["Toppings"])
			{
				Topping _topping = context.Toppings.Find(Convert.ToInt32(topping));
				price += _topping.Price;
				ToppingPizza toppingPizza = new ToppingPizza
				{
					PizzaId = pizza.Id,
					ToppingId = Convert.ToInt32(topping)
				};
				context.ToppingPizzas.Add(toppingPizza);
			}
			switch (pizza.Size)
			{
			case 8:
				price += 7.99;
				break;
			case 12:
				price += 12.99;
				break;
			case 16:
				price += 17.99;
				break;
			}
			if (pizza.Crust == 2)
			{
				price += 2.99;
			}
			pizza.Price = price;
			context.Update(pizza);
			Order order = context.Orders.Find(id);
			order.Price += price;
			context.Orders.Update(order);
			context.SaveChanges();
			return RedirectToAction("Index");
		}
		public IActionResult Edit(int id)
		{
			ViewData["hasOrder"] = OrderController.checkForOrder(context);
			Pizza pizza = context.Pizzas.Include(p => p.Toppings).ThenInclude(t => t.Topping).FirstOrDefault(p => p.Id == id);
			return View(pizza);
		}
		
		[HttpPost]
		public IActionResult Edit(int id, IFormCollection form) 
		{
			Pizza pizza = context.Pizzas.Include(p => p.Toppings).ThenInclude(t => t.Topping).FirstOrDefault(p => p.Id == id);
			pizza.Size = Convert.ToInt32(form["Size"]);
			pizza.Crust = Convert.ToInt32(form["Crust"]);
			double price = 0;
			foreach(var topping in form["Toppings"])
			{
				Topping _topping = context.Toppings.Find(Convert.ToInt32(topping));
				bool found = false;
				foreach(var _toppingPizza in pizza.Toppings)
				{
					if (_toppingPizza.ToppingId == _topping.Id)
					{
						found = true;
						break;
					}
				}
				if(found)
				{
					continue;
				}
				price += _topping.Price;
				ToppingPizza toppingPizza = new ToppingPizza
				{
					PizzaId = pizza.Id,
					ToppingId = Convert.ToInt32(topping)
				};
				context.ToppingPizzas.Add(toppingPizza);
			}
			foreach(var topping in pizza.Toppings) 
			{
				bool found = false;
				foreach(var _topping in form["Toppings"])
				{
					if (topping.ToppingId == Convert.ToInt32(_topping))
					{
						found = true;
						break;
					}
				}
				if (!found)
				{
					ToppingPizza toppingPizza = context.ToppingPizzas.FirstOrDefault(tp => tp.ToppingId == topping.ToppingId && tp.PizzaId == pizza.Id);
					context.ToppingPizzas.Remove(toppingPizza);
				}
			}
			switch (pizza.Size)
			{
			case 8:
				price += 7.99;
				break;
			case 12:
				price += 12.99;
				break;
			case 16:
				price += 17.99;
				break;
			}
			if (pizza.Crust == 2)
			{
				price += 2.99;
			}
			pizza.Price = price;
			context.Update(pizza);
			Order order = context.Orders.Find(pizza.OrderId);
			order.Price += price;
			context.Orders.Update(order);
			context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}

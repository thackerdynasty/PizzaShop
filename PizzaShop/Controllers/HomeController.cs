using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PizzaShop.Models;

namespace PizzaShop.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;
	AppDBContext context;
	
	public HomeController(AppDBContext context, ILogger<HomeController> logger)
	{
		_logger = logger;
		this.context = context;
	}

	public IActionResult Index()
	{
		ViewData["hasOrder"] = OrderController.checkForOrder(context);
		return View();
	}

	public IActionResult Privacy()
	{
		ViewData["hasOrder"] = OrderController.checkForOrder(context);
		return View();
	}

	[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
	public IActionResult Error()
	{
		return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
	}
}

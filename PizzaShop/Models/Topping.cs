using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Models;

public class Topping 
{
	[Required]
	public string Name { get; set; }

	[Required]
	public double Price { get; set; }
	
	[Key]
	public int Id { get; set; }
	
	public List<ToppingPizza> OnPizzas { get; set; }
}
using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaShop.Models;

public class Pizza 
{
	[ForeignKey("Order")]
	public int OrderId { get; set; }
	
	[Key]
	public int Id { get; set; }
	
	[Required]
	public double Price { get; set; }
	
	// in inches
	[Required]
	public int Size { get; set; }
	
	// 0 = thin, 1 = normal, 2 = stuffed
	[Required]
	public int Crust { get; set; }
	
	public List<ToppingPizza> Toppings { get; set; }
	
	public Order Order { get; set; }
}
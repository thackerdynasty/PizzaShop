using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaShop.Models;

public class ToppingPizza 
{
	[ForeignKey("Pizza")]
	public int PizzaId { get; set; }
	
	[ForeignKey("Topping")]
	public int ToppingId { get; set; }
	
	[Key]
	public int Id { get; set; }
	
	public Pizza Pizza { get; set; }
	
	public Topping Topping { get; set; }
}
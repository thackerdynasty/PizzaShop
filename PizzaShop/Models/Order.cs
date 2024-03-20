using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaShop.Models;

public class Order 
{
	[ForeignKey("User")]
	public int UserId { get; set; }
	
	[Required]
	public double Price { get; set; }
	
	[Key]
	public int Id { get; set; }
	
	[Required]
	public int Status { get; set; }
	
	public List<Pizza> Pizzas { get; set; }
	
	public User User { get; set; }
}
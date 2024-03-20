using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace PizzaShop.Models;

public class User
{
	[Required]
	public string Username { get; set; }

	[Required]
	[EmailAddress]
	public string Email { get; set; }
	
	[Key]
	public int Id { get; set; }
	
	public List<Order> Orders { get; set; }
}

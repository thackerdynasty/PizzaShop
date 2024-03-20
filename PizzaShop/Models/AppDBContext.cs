using Microsoft.EntityFrameworkCore;

namespace PizzaShop.Models
{
	public class AppDBContext : DbContext
	{
		public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
		{}
		
		public DbSet<User> Users { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<Pizza> Pizzas { get; set; }
		public DbSet<Topping> Toppings { get; set; }
		public DbSet<ToppingPizza> ToppingPizzas { get; set; }
	}
}

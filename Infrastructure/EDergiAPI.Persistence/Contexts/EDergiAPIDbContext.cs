using Microsoft.EntityFrameworkCore;
using EDergiAPI.Domain.Entitites;
using System.Reflection;

namespace EDergiAPI.Persistence.Contexts
{
	public class EDergiAPIDbContext : DbContext
	{
		
		public EDergiAPIDbContext(DbContextOptions<EDergiAPIDbContext> options) : base(options)
		{
		}

		
		public DbSet<Product> Products { get; set; }
		public DbSet<Customer> Customers { get; set; }
		public DbSet<deneme> denemes { get; set; }

		
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(modelBuilder);
		}
	}
}

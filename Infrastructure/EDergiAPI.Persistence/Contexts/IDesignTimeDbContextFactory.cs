using Microsoft.EntityFrameworkCore;  
using Microsoft.EntityFrameworkCore.Design;  
using Microsoft.Extensions.Configuration;  
using System.IO;  
namespace DergiAPI.Persistence.Contexts  
{  
	public class EDergiAPIDbContextFactory : IDesignTimeDbContextFactory<EDergiAPIDbContext>  
	{  
		public EDergiAPIDbContext CreateDbContext(string[] args)  
		{  
			IConfigurationRoot configuration = new ConfigurationBuilder()  
				.SetBasePath(Directory.GetCurrentDirectory())  
				.AddJsonFile("appsettings.json")  
				.Build();  

			var optionsBuilder = new DbContextOptionsBuilder<EDergiAPIDbContext>();  
			var connectionString = configuration.GetConnectionString("SqlServer");  

			optionsBuilder.UseSqlServer(connectionString);  

			return new EDergiAPIDbContext(optionsBuilder.Options);  
		}  
	}  
}  
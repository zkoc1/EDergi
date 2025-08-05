using Microsoft.EntityFrameworkCore;  
using Microsoft.EntityFrameworkCore.Design;  
using Microsoft.Extensions.Configuration;  
using System.IO;  
namespace EDergi.Persistence.Contexts  
{  
	public class EDergiDbContextFactory : IDesignTimeDbContextFactory<EDergiDbContext>  
	{  
		public EDergiDbContext CreateDbContext(string[] args)  
		{  
			IConfigurationRoot configuration = new ConfigurationBuilder()  
				.SetBasePath(Directory.GetCurrentDirectory())  
				.AddJsonFile("appsettings.json")  
				.Build();  

			var optionsBuilder = new DbContextOptionsBuilder<EDergiDbContext>();  
			var connectionString = configuration.GetConnectionString("SqlServer");  

			optionsBuilder.UseSqlServer(connectionString);  

			return new EDergiDbContext(optionsBuilder.Options);  
		}  
	}  
}  
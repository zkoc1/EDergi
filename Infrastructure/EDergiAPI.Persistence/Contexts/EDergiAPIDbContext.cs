using DergiAPI.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Reflection.Metadata;

namespace DergiAPI.Persistence.Contexts
{
	public class EDergiAPIDbContext : DbContext
	{
		public EDergiAPIDbContext(DbContextOptions<EDergiAPIDbContext> options) : base(options)
		{
		}

		// Dergi ile ilgili entity'ler
		public DbSet<Article> Articles { get; set; }//
		public DbSet<Author> Authors { get; set; }//
	    
		public DbSet<MDocument> Documents { get; set; }//
		public DbSet<ReadIndex> ReadIndices { get; set; }//
	
		public DbSet<Magazine> Magazines { get; set; }//
		public DbSet<Publisher> Publishers { get; set; }//
		






		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Konfigürasyonları uygula
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(modelBuilder);
		}
	}
}

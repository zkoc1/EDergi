using EDergiAPI.Application.Abstractions;
using EDergiAPI.Domain.Entitites;
using EDergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDergiAPI.Persistence.Concretes
{
	public class ProductService : IProductService
	{
		public List<Product> GetProducts()
		{
			throw new NotImplementedException();
		}

		public async Task<List<Product>> GetProductsAsync()
		{
			// Örnek ürün listesi
			return await Task.FromResult(new List<Product>
			{
				new Product
				{
					Id = Guid.NewGuid(),
					Name = "p1",
					Price = 100,
					Stock = 10
				},
				new Product
				{
					Id = Guid.NewGuid(),
					Name = "p2",
					Price = 200,
					Stock = 10
				},
				new Product
				{
					Id = Guid.NewGuid(),
					Name = "p3",
					Price = 300,
					Stock = 10
				},
				new Product
				{
					Id = Guid.NewGuid(),
					Name = "p4",
					Price = 400,
					Stock = 10
				},
				new Product
				{
					Id = Guid.NewGuid(),
					Name = "p5",
					Price = 500,
					Stock = 10
				}
			});
		}
	}
}

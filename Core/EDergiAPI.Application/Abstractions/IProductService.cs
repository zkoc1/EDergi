using EDergiAPI.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDergiAPI.Application.Abstractions
{
	public interface IProductService
	{
		List<Product> GetProducts();

	}
}

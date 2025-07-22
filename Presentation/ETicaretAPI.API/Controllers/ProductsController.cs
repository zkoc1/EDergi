using ETicaretAPI.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
	public class ProductsController : Controller
	{
		 private readonly IProductService _productService;

		public ProductsController(IProductService productService)
		{
			_productService = productService;
		}
		[HttpGet]
		public IActionResult GetProdocts()
		{
			  var products = _productService.GetProducts();
			return Ok(products);
		}
	}
}

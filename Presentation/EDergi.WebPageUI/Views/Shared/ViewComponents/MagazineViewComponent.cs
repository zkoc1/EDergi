using EDergi.Application.Abstractions;
using EDergi.Application.ViewComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace EDergi.WebPageUI.Views.Shared.ViewComponents
{
	public class MagazineViewComponent : ViewComponent
	{
		private readonly IMagazineService _magazineService;

		public MagazineViewComponent(IMagazineService magazineService)
		{
			_magazineService = magazineService;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var magazines = await _magazineService.GetAllAsync2();
			return View(magazines); // ✅ artık List<MagazineCreateDto> döndürüyor
		}
	}
}

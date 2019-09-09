using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCore.Application.Interfaces;
using WebAppCore.Models;

namespace WebAppCore.Controllers.Components
{
	public class MainMenuViewComponent:ViewComponent
	{
		private ITypeService _typeService;
		public MainMenuViewComponent(ITypeService typeService)
		{
			_typeService = typeService;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			var data = new TypeVM();
			data.typeViewModels = await _typeService.GetAll();
			return View(data);
		}
	}
}

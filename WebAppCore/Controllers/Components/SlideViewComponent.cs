using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCore.Application.Interfaces;
using WebAppCore.Models;

namespace WebAppCore.Controllers.Components
{
	public class SlideViewComponent:ViewComponent
	{
		private ITypeService _typeService;
		public SlideViewComponent(ITypeService typeService)
		{
			_typeService = typeService;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View();
		}
	}
}

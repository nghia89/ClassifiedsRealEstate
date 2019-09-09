using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebAppCore.Application.Interfaces;
using WebAppCore.Application.ViewModels;
using WebAppCore.Application.ViewModels.General;
using WebAppCore.Utilities.Helpers;

namespace WebAppCore.Areas.Admin.Controllers
{
    public class TypeController :BaseController
	{
		private ITypeService _typeService;

		public TypeController(ITypeService typeService)
		{
			_typeService = typeService;
		}

		public IActionResult Index()
		{
			return View();
		}

		//Ajax Api
		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			var model =await _typeService.GetAll();
			return new OkObjectResult(model);
		}

		[HttpGet]
		public async Task<IActionResult> GetById(int id)
		{
			var model = await _typeService.GetById(id);

			return new ObjectResult(model);
		}


		[HttpPost]
		public IActionResult SaveEntity(TypeViewModel productVm)
		{
			if(!ModelState.IsValid)
			{

				IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
				ErrorMesage e = new ErrorMesage();
				if(productVm.SortOrder == 0)
				{
					e.Message = "Vui lòng nhập SortOrder";
					e.Error = true;
					return new BadRequestObjectResult(e);
				}

				return new BadRequestObjectResult(allErrors);
			}
			else
			{
				productVm.SeoAlias = TextHelper.ToUnsignString(productVm.Name);
				if(productVm.Id == 0)
				{
					_typeService.Add(productVm);
				}
				else
				{
					_typeService.Update(productVm);
				}
				_typeService.Save();
				return new OkObjectResult(productVm);
			}
		}

		[HttpPost]
		public IActionResult Delete(int id)
		{
			if(id == 0)
			{
				return new BadRequestResult();
			}
			else
			{
				_typeService.Delete(id);
				_typeService.Save();
				return new OkObjectResult(id);
			}
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebAppCore.Application.Interfaces;
using WebAppCore.Application.ViewModels.Blog;
using WebAppCore.Utilities.Helpers;

namespace WebAppCore.Areas.Admin.Controllers
{
    public class ClassifiedsController :BaseController
	{
		public IClassifiedsService _classifiedsService;

		public ClassifiedsController(IClassifiedsService classifiedsService)
		{
			_classifiedsService = classifiedsService;
		}

		public IActionResult Index()
		{
			return View();
		}
		public IActionResult GetAll()
		{
			var model = _classifiedsService.GetAll();
			return new OkObjectResult(model);
		}
		[HttpGet]
		public async Task<IActionResult> GetById(int id)
		{
			var model =await _classifiedsService.GetById(id);
			return new OkObjectResult(model);
		}
		[HttpGet]
		public async Task<IActionResult> GetAllPaging(string keyword,int page,int pageSize)
		{
			var model =await _classifiedsService.GetAllPaging(keyword,page,pageSize);
			return new OkObjectResult(model);
		}
		[HttpPost]
		public IActionResult SaveEntity(ClassifiedsViewModel pageVm)
		{
			if(!ModelState.IsValid)
			{
				IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
				return new BadRequestObjectResult(allErrors);
			}
			if(pageVm.Id == 0)
			{
				pageVm.SeoAlias = TextHelper.ToUnsignString(pageVm.Title);
				_classifiedsService.Add(pageVm);
			}
			else
			{
				pageVm.SeoAlias = TextHelper.ToUnsignString(pageVm.Title);
				_classifiedsService.Update(pageVm);
			}
			_classifiedsService.Save();
			return new OkObjectResult(pageVm);
		}
		[HttpPost]
		public IActionResult Delete(int id)
		{
			if(!ModelState.IsValid)
			{
				return new BadRequestObjectResult(ModelState);
			}
			_classifiedsService.Delete(id);
			_classifiedsService.Save();
			return new OkObjectResult(id);
		}
	}
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;
using WebAppCore.Application.Interfaces;
using WebAppCore.Application.ViewModels.Blog;
using WebAppCore.Models;
using WebAppCore.Utilities.Dtos;
using WebAppCore.Views;

namespace WebAppCore.Controllers
{
    public class ClassifiedsController : Controller
    {
		private IClassifiedsService _classifiedsService;
		private readonly IConfiguration _configuration;
		public ClassifiedsController(IClassifiedsService classifiedsServicee,IConfiguration configuration)
		{
			_classifiedsService = classifiedsServicee;
			_configuration = configuration;
		}

		[Route("{alias}-c.{id}.html")]
		public async Task<IActionResult> Index(int id,string keywork,int? pageSize,int page = 1)
		{
			var classified = new ClassifiedsVM();
			if(pageSize == null)
				pageSize = _configuration.GetValue<int>("PageSize");
			classified.PageSize = pageSize;
			var data =await _classifiedsService.GetAllPaging(id,keywork,page,pageSize.Value);
			var listData = data.Results.Select(a => ClassifiedsViewModel.form(a)).ToList();
			var paginationSet = new PagedResult<ClassifiedsViewModel>() {
				Results = listData,
				CurrentPage = page,
				RowCount = data.RowCount,
				PageSize = data.PageSize,
			};
			classified.Data = paginationSet;
			return View(classified);
		}

		[Route("tin-moi.html",Name = "viewAll")]
		public async Task<IActionResult> ViewAll(string keywork,int? pageSize=2,int page = 1)
		{
			var classified = new ClassifiedsVM();
			if(pageSize == null)
				pageSize = _configuration.GetValue<int>("PageSize");
			classified.PageSize = pageSize;
			var data = await _classifiedsService.GetViewAll(keywork,page,pageSize.Value);
			var listData = data.Results.Select(a => ClassifiedsViewModel.form(a)).ToList();
			var paginationSet = new PagedResult<ClassifiedsViewModel>() {
				Results = listData,
				CurrentPage = page,
				RowCount = data.RowCount,
				PageSize = data.PageSize,
			};
			classified.Data = paginationSet;
			return View(classified);
		}

		[Route("{SeoAlias}-p.{id}.html",Name = "detail")]
		public async Task<IActionResult> Detail(int id)
		{
			var dataVM = new ClassifiedsDetailVM();
			var data =await _classifiedsService.GetById(id);
			var RelatedBlogs = _classifiedsService.RelatedBlog(id,8);
			dataVM.Data = ClassifiedsViewModel.form(data);
			dataVM.RelatedBlogs = RelatedBlogs;
			return View(dataVM);
		}
	}
}
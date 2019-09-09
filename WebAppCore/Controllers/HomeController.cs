using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebAppCore.Application.Interfaces;
using WebAppCore.Application.ViewModels.Blog;
using WebAppCore.Models;
using WebAppCore.Utilities.Dtos;
using WebAppCore.Views;

namespace WebAppCore.Controllers
{
	public class HomeController:Controller
	{

		private IClassifiedsService _classifiedsService;
		private readonly IConfiguration _configuration;
		public HomeController(IClassifiedsService classifiedsServicee,IConfiguration configuration)
		{
			_classifiedsService = classifiedsServicee;
			_configuration = configuration;
		}

		public async Task<IActionResult> Index(string keywork,int? pageSize,int page = 1)
		{
			var classified = new ClassifiedsDetailVM();
			var data = await _classifiedsService.GetHotProduct(8);
			var listData = data.Select(a => ClassifiedsViewModel.form(a)).ToList();
			classified.RelatedBlogs = listData;
			return View(classified);
		}
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
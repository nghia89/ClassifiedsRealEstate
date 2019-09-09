using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCore.Application.ViewModels.Blog;
using WebAppCore.Utilities.Dtos;

namespace WebAppCore.Models
{
	public class ClassifiedsVM
	{
		public PagedResult<ClassifiedsViewModel> Data { get; set; }
		public int? PageSize { set; get; }
	}

	public class ClassifiedsDetailVM
	{
		public ClassifiedsViewModel Data { get; set; }
		public List<ClassifiedsViewModel> RelatedBlogs { get; set; }

	}
}

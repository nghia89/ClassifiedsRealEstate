using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebAppCore.Application.ViewModels.Blog;
using WebAppCore.Application.ViewModels.Common;
using WebAppCore.Utilities.Dtos;

namespace WebAppCore.Application.Interfaces
{
	public interface IClassifiedsService
	{
		ClassifiedsViewModel Add(ClassifiedsViewModel product);

		void Update(ClassifiedsViewModel product);

		void Delete(int id);

		List<ClassifiedsViewModel> GetAll();

		Task<PagedResult<ClassifiedsViewModel>> GetAllPaging(string keyword,int pageSize,int page);

		Task<PagedResult<ClassifiedsViewModel>> GetAllPaging(int id,string keyword,int page,int pageSize);

		Task<List<ClassifiedsViewModel>> GetLastest(int top);

		Task<List<ClassifiedsViewModel>> GetHotProduct(int top);

		List<ClassifiedsViewModel> GetListPaging(int page,int pageSize,string sort,out int totalRow);

		List<ClassifiedsViewModel> Search(string keyword,int page,int pageSize,string sort,out int totalRow);

		List<ClassifiedsViewModel> GetList(string keyword);

		List<ClassifiedsViewModel> GetReatedBlogs(int id,int top);

		List<string> GetListByName(string name);

		Task<ClassifiedsViewModel> GetById(int id);

		List<ClassifiedsViewModel> RelatedBlog(int id,int top);

		void Save();

		List<TagViewModel> GetListTagById(int id);

		TagViewModel GetTag(string tagId);

		void IncreaseView(int id);

		List<ClassifiedsViewModel> GetListByTag(string tagId,int page,int pagesize,out int totalRow);

		List<TagViewModel> GetListTag(string searchText);

		Task<PagedResult<ClassifiedsViewModel>> GetViewAll(string keyword,int page,int pageSize);
	}
}

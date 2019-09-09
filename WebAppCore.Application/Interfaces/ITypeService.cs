using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebAppCore.Application.ViewModels.General;

namespace WebAppCore.Application.Interfaces
{
	public interface ITypeService
	{
		TypeViewModel Add(TypeViewModel productCategoryVm);

		void Update(TypeViewModel productCategoryVm);

		void Delete(int id);

		Task<List<TypeViewModel>> GetAll();

		List<TypeViewModel> GetAll(string keyword);

		List<TypeViewModel> GetAllByParentId(int parentId);

		Task<TypeViewModel> GetById(int id);

		void UpdateParentId(int sourceId,int targetId,Dictionary<int,int> items);

		void ReOrder(int sourceId,int targetId);

		Task<List<TypeViewModel>> GetHomeCategories(int top);

		void Save();
	}
}

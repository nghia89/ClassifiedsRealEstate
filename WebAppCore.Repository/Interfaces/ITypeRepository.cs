using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Type = WebAppCore.Data.Entities.Type;

namespace WebAppCore.Repository.Interfaces
{
	public interface ITypeRepository
	{
		Task<List<Type>> GetAll();

		Task<Type> GetById(long id);

		Task<(List<Type> data, long totalCount)> Paging(int page,int page_size);

		Task<List<Type>> GetHomeCategories(int top);

		Task<List<Type>> GetProductNew(int top);
	}
}

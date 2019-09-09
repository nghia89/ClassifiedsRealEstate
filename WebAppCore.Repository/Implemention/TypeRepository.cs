using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppCore.Data.EF;
using WebAppCore.Data.Entities;
using WebAppCore.Data.Enums;
using WebAppCore.Infrastructure.Interfaces;
using WebAppCore.Repository.Interfaces;
using Type = WebAppCore.Data.Entities.Type;

namespace WebAppCore.Repository.Implemention
{
	public class TypeRepository:ITypeRepository
	{
		private AppDbContext _appDbContext;
		private IRepository<Type,int> _typeCategory;
		private IRepository<Classifieds,int> _classifiedsRepository;
		public TypeRepository(AppDbContext appDbContext,IRepository<Type,int> typeCategory,
			IRepository<Classifieds,int> classifiedsRepository)
		{
			this._appDbContext = appDbContext;
			this._typeCategory = typeCategory;
			this._classifiedsRepository = classifiedsRepository;
		}
		public async Task<List<Type>> GetAll()
		{
			var data = await _typeCategory.FindAllAsync(x => x.Status == Status.Active);
			return data.ToList();
		}

		public async Task<Type> GetById(long id)
		{
			var data = await _typeCategory.GetAByIdIncludeAsyn(x => x.Id == id);
			return data;
		}

		public Task<List<Type>> GetHomeCategories(int top)
		{
			throw new NotImplementedException();
		}

		public Task<List<Type>> GetProductNew(int top)
		{
			throw new NotImplementedException();
		}

		public Task<(List<Type> data, long totalCount)> Paging(int page,int page_size)
		{
			throw new NotImplementedException();
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppCore.Data.Entities;
using WebAppCore.Data.Enums;
using WebAppCore.Infrastructure.Interfaces;
using WebAppCore.Repository.Interfaces;

namespace WebAppCore.Repository.Implemention
{
	public class ClassifiedsRepository:IClassifiedsRepository
	{
		private IRepository<Classifieds,int> _repository;
		public ClassifiedsRepository(IRepository<Classifieds,int> repository)
		{
			this._repository = repository;
		}
		public async Task<List<Classifieds>> GetLastest(int top)
		{
			var data = await _repository.FindAllAsync(x => x.Status == Status.Active);
			return data.OrderByDescending(x => x.DateCreated)
			   .Take(top).ToList();
		}
	}
}

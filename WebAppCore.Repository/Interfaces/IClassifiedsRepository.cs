using System.Collections.Generic;
using System.Threading.Tasks;
using WebAppCore.Data.Entities;

namespace WebAppCore.Repository.Interfaces
{
	public interface IClassifiedsRepository
	{
		Task<List<Classifieds>> GetLastest(int top);

	}
}

using AutoMapper;
using System.Linq;
using WebAppCore.Application.ViewModels.General;
using Type = WebAppCore.Data.Entities.Type;

namespace WebAppCore.Application.Mappers
{
	public class TypeMapperProfile:Profile
	{
		public TypeMapperProfile()
		{
			CreateMap<Type,TypeViewModel>().ForMember(a => a.Classifieds,o => o.ResolveUsing(b => b.Classifieds != null
				? b.Classifieds.Select(c => c.ToModel()).ToList() : null)).MaxDepth(2).ReverseMap();
		}
	}
	public static class TypeMapper
	{
		internal static IMapper Mapper { get; }

		static TypeMapper()
		{
			Mapper = new MapperConfiguration(cfg => cfg.AddProfile<TypeMapperProfile>())
			   .CreateMapper();
		}

		public static TypeViewModel ToModel(this Type Type)
		{
			return Mapper.Map<TypeViewModel>(Type);
		}

		//public static Product ToModel(this Type productVm)
		//{
		//	return Mapper.Map<Product>(productVM);
		//}

		public static Type AddModel(this TypeViewModel addModel)
		{
			return Mapper.Map<Type>(addModel);
		}
	}
}

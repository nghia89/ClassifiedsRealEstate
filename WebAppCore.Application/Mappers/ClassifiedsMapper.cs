using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebAppCore.Application.ViewModels.Blog;
using WebAppCore.Data.Entities;

namespace WebAppCore.Application.Mappers
{
	public class ClassifiedsMapperProfile:Profile
	{
		public ClassifiedsMapperProfile()
		{
			CreateMap<Classifieds,ClassifiedsViewModel>().ForMember(a=>a.ClassifiedsTag,o=>o.ResolveUsing(b => b.ClassifiedsTags == null
				? null : b.ClassifiedsTags.Select(c => c.ToModel()).ToList())).MaxDepth(2)
				.ReverseMap();
		}
	}
	public static class ClassifiedsMapperMapper
	{
		internal static IMapper Mapper { get; }

		static ClassifiedsMapperMapper()
		{
			Mapper = new MapperConfiguration(cfg => cfg.AddProfile<ClassifiedsMapperProfile>())
			   .CreateMapper();
		}

		public static ClassifiedsViewModel ToModel(this Classifieds classifieds)
		{
			return Mapper.Map<ClassifiedsViewModel>(classifieds);
		}

		public static Classifieds AddModel(this ClassifiedsViewModel classifieds)
		{
			return Mapper.Map<Classifieds>(classifieds);
		}
	}
}

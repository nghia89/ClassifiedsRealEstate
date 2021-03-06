﻿using AutoMapper;
using WebAppCore.Application.ViewModels.Blog;
using WebAppCore.Data.Entities;

namespace WebAppCore.Application.Mappers
{
	public class BlogTagMapperProfile:Profile
	{
		public BlogTagMapperProfile()
		{
			CreateMap<ClassifiedsTag,BlogTagViewModel>().ReverseMap();
		}
	}
	public static class BlogTagMapper
	{
		internal static IMapper Mapper { get; }

		static BlogTagMapper()
		{
			Mapper = new MapperConfiguration(cfg => cfg.AddProfile<BlogTagMapperProfile>())
			   .CreateMapper();
		}

		public static BlogTagViewModel ToModel(this ClassifiedsTag blogTag)
		{
			return Mapper.Map<BlogTagViewModel>(blogTag);
		}
	}
}

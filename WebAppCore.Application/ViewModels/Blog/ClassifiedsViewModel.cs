using System;
using System.Collections.Generic;
using WebAppCore.Data.Entities;
using WebAppCore.Data.Enums;
using WebAppCore.Utilities.Extensions;
using Type = WebAppCore.Data.Entities.Type;
namespace WebAppCore.Application.ViewModels.Blog
{
	public class ClassifiedsViewModel
	{
		public int Id { set; get; }
		public string Title { get; set; }
		public int? TypeId { get; set; }
		public int? ClassifiedsAddressId { get; set; }
		public decimal? Area { get; set; }//diện tích
		public decimal? Price { get; set; }
		public string Image { set; get; }
		public string Content { set; get; }
		public string Description { get; set; }
		public decimal? Front { get; set; }//mặt trước 
		public decimal? WayIn { get; set; }//mặt sau
		public string MainDirection { get; set; }// hướng nhà
		public string BalconyDirection { get; set; }//ban công
		public int? Floor { get; set; }//sàn nhà
		public int? BedRoom { get; set; }//giường ngủ
		public int? RestRoom { get; set; }//nhà vs
		public string Furniture { get; set; }//nội thất
		public string ImageList { get; set; }
		public float? Longtitude { get; set; }//kinh độ
		public float? Latitude { get; set; }//vĩ độ
		public bool? HomeFlag { set; get; }
		public bool? HotFlag { set; get; }
		public int? ViewCount { set; get; }
		public string Tags { get; set; }
		public List<ClassifiedsTag> ClassifiedsTag { set; get; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }
		public string SeoPageTitle { set; get; }
		public string SeoAlias { set; get; }
		public string SeoKeywords { set; get; }
		public string SeoDescription { set; get; }
		public Status Status { get; set; }
		public string TypeName { get; set; }
		public Type Type { get; set; }

		public string TimeAgo { get; set; }

		public static ClassifiedsViewModel form(ClassifiedsViewModel model)
		{
			if(model == null) return null;
			ClassifiedsViewModel result = new ClassifiedsViewModel {
				Id = model.Id,
				Area = model.Area,
				BalconyDirection = model.BalconyDirection,
				BedRoom = model.BedRoom,
				MainDirection = model.MainDirection,
				ClassifiedsAddressId = model.ClassifiedsAddressId,
				Floor = model.Floor,
				Front = model.Front,
				Furniture = model.Furniture,
				ImageList = model.ImageList,
				Latitude = model.Latitude,
				Longtitude = model.Longtitude,
				Price = model.Price,
				RestRoom = model.RestRoom,
				Title = model.Title,
				WayIn = model.WayIn,
				ClassifiedsTag = model.ClassifiedsTag,
				Content = model.Content,
				DateCreated = model.DateCreated,
				DateModified = model.DateModified,
				Description = model.Description,
				HomeFlag = model.HomeFlag,
				HotFlag = model.HotFlag,
				Image = model.Image,
				SeoAlias = model.SeoAlias,
				SeoDescription = model.SeoDescription,
				SeoKeywords = model.SeoKeywords,
				SeoPageTitle = model.SeoPageTitle,
				Status = model.Status,
				Tags = model.Tags,
				TypeName=model.Type?.Name,
				Type=model.Type,
				TimeAgo = RelativeDate.TimeAgo(model.DateCreated)
			};
			return result;
		}
	}
}

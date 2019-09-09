using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebAppCore.Data.Enums;
using WebAppCore.Data.Interfaces;
using WebAppCore.Infrastructure.SharedKernel;

namespace WebAppCore.Data.Entities
{
	[Table("Classifieds")]
	public class Classifieds:DomainEntity<int>, IDateTracking, ISwitchable
	{
		public string Title { get; set; }
		public int? TypeId { get; set; }
		public Type Type { get; set; }
		public int? ClassifiedsAddressId { get; set; }
		public decimal? Area { get; set; }//diện tích
		public decimal Price { get; set; }
		public string Image { set; get; }
		public string Content { set; get; }
		public string Description { get; set; }
		public decimal? Front { get; set; }
		public decimal? WayIn { get; set; }
		public string MainDirection { get; set; }// hướng nhà
		public string BalconyDirection { get; set; }//ban công
		public int? Floor { get; set; }//Số tầng
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
		public virtual ICollection<ClassifiedsTag> ClassifiedsTags { set; get; }
		//public string ContactName { get; set; }
		//public string ContactAddress { get; set; }//đ/c liên hệ
		//public string ContactPhone { get; set; }//sđt liên hệ
		//public string ContactEmail { get; set; }//sđt liên hệ
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }
		public string SeoPageTitle { set; get; }
		public string SeoAlias { set; get; }
		public string SeoKeywords { set; get; }
		public string SeoDescription { set; get; }
		public Status Status { get; set; }
	}
}

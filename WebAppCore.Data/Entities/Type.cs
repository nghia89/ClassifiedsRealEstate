using System;
using System.Collections.Generic;
using System.Text;
using WebAppCore.Data.Enums;
using WebAppCore.Data.Interfaces;
using WebAppCore.Infrastructure.SharedKernel;

namespace WebAppCore.Data.Entities
{
	public class Type:DomainEntity<int>, ISwitchable
	{
		public string Name { get; set; }
		public string Image { get; set; }
		public bool? HomeFlag { get; set; }
		public int? ParentId { get; set; }
		public DateTime DateCreated { set; get; }
		public DateTime DateModified { set; get; }
		public Status Status { set; get; }
		public string SeoPageTitle { set; get; }
		public string SeoAlias { set; get; }
		public string SeoKeywords { set; get; }
		public string SeoDescription { set; get; }
		public int SortOrder { set; get; }
		public virtual ICollection<Classifieds> Classifieds { set; get; }
	}
}

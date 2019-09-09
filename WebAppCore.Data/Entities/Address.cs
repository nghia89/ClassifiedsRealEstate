using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebAppCore.Data.Enums;
using WebAppCore.Data.Interfaces;
using WebAppCore.Infrastructure.SharedKernel;

namespace WebAppCore.Data.Entities
{
	public class Address:DomainEntity<int>,IDateTracking, ISwitchable
	{
		public int CityId { get; set; }
		public int DistrictId { get; set; }
		public int WardId { get; set; }
		public virtual City City { get; set; }
		public virtual District District { get; set; }
		public virtual Ward Ward { get; set; }
		public string Street { get; set; }
		public Status Status { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }
	}
}

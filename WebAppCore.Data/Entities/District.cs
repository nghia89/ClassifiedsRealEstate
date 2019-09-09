using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebAppCore.Infrastructure.SharedKernel;

namespace WebAppCore.Data.Entities
{
	public class District:DomainEntity<int>
	{
		public int CityId { get; internal set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public virtual City City { get; set; }
		public virtual List<Ward> Wards { get; set; }
		public string OtherName { get; set; }
	}
}

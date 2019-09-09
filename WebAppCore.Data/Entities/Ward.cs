using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using WebAppCore.Infrastructure.SharedKernel;

namespace WebAppCore.Data.Entities
{
	public class Ward:DomainEntity<int>
	{
		public string Code { get; set; }
		public int DistrictId { get; internal set; }
		public string Name { get; set; }
		public virtual District District { get; set; }
		public string OtherName { get; set; }
	}
}

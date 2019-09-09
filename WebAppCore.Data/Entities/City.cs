using System;
using System.Collections.Generic;
using System.Text;
using WebAppCore.Infrastructure.SharedKernel;

namespace WebAppCore.Data.Entities
{
	public class City:DomainEntity<int>
	{
		public string Code { get; set; }
		public string Name { get; set; }
		public virtual List<District> Districts { get; set; }
		public string OtherName { get; set; }
	}
}

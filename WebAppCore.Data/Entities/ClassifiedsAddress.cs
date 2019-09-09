using System;
using System.Collections.Generic;
using System.Text;
using WebAppCore.Data.Enums;
using WebAppCore.Data.Interfaces;
using WebAppCore.Infrastructure.SharedKernel;

namespace WebAppCore.Data.Entities
{
	public class ClassifiedsAddress:DomainEntity<int>,IDateTracking, ISwitchable
	{
		public long? ClassifiedsId { get; set; }
		public Classifieds Classifieds { get; set; }
		public long? AddressId { get; set; }
		public Address Address { get; set; }
		public DateTime DateCreated { get; set; }
		public DateTime DateModified { get; set; }
		public Status Status { get; set; }
	}
}

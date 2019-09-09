using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebAppCore.Data.Enums;

namespace WebAppCore.Application.ViewModels.Blog
{
    public class ProductGiftViewModel
    {
        public int Id { set; get; }

		[Required]
		[MaxLength(256)]
		public string Name { set; get; }

		[Required]
		public string Content { set; get; }

		public Status Status { set; get; }

	}
}

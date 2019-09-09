using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCore.Application.ViewModels.Product;

namespace WebAppCore.Models.ProductViewModels
{
    public class DetailViewModel
    {

        public List<ProductImageViewModel> ProductImages { set; get; }

        public List<TagViewModel> Tags { set; get; }

        public List<SelectListItem> Colors { set; get; }

        public List<SelectListItem> Sizes { set; get; }
    }
}

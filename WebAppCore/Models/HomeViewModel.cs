using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCore.Application.ViewModels.Blog;
using WebAppCore.Application.ViewModels.Common;
using WebAppCore.Application.ViewModels.Product;

namespace WebAppCore.Models
{
    public class HomeViewModel
    {
        public List<SlideShowViewModel> HomeSlides { get; set; }

        public string Title { set; get; }
        public string MetaKeyword { set; get; }
        public string MetaDescription { set; get; }
    }
}

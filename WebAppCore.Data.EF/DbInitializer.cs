﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCore.Data.Entities;
using WebAppCore.Data.Enums;
using WebAppCore.Utilities.Constants;

namespace WebAppCore.Data.EF
{
	public class DbInitializer
	{
		private readonly AppDbContext _context;
		private UserManager<AppUser> _userManager;
		private RoleManager<AppRole> _roleManager;
		public DbInitializer(AppDbContext context,UserManager<AppUser> userManager,RoleManager<AppRole> roleManager)
		{
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task Seed()
		{
			if(!_roleManager.Roles.Any())
			{
				await _roleManager.CreateAsync(new AppRole() {
					Name = "Admin",
					NormalizedName = "Admin",
					Description = "Top manager"
				});
				await _roleManager.CreateAsync(new AppRole() {
					Name = "Staff",
					NormalizedName = "Staff",
					Description = "Staff"
				});
				await _roleManager.CreateAsync(new AppRole() {
					Name = "Customer",
					NormalizedName = "Customer",
					Description = "Customer"
				});
			}
			if(!_userManager.Users.Any())
			{
				await _userManager.CreateAsync(new AppUser() {
					UserName = "admin",
					FullName = "Administrator",
					Email = "admin@gmail.com",
					Balance = 0,
					DateCreated = DateTime.Now,
					DateModified = DateTime.Now,
					Status = Status.Active
				},"123654$");
				var user = await _userManager.FindByNameAsync("admin");
				await _userManager.AddToRoleAsync(user,"Admin");
			}
			if(!_context.Contacts.Any())
			{
				_context.Contacts.Add(new Contact() {
					Id = CommonConstants.DefaultContactId,
					Address = "My Address",
					Email = "MyShop@gmail.com",
					Name = "My Shop",
					Phone = "0971669906",
					Status = Status.Active,
					Website = "http://myshop.com",
					Lat = 10.8306206,
					Lng = 106.7726841
				});
			}
			if(_context.Functions.Count() == 0)
			{
				_context.Functions.AddRange(new List<Function>()
				{
					new Function() {Id = "SYSTEM", Name = "System",ParentId = null,SortOrder = 1,Status = Status.Active,URL = "/",IconCss = "fa-desktop"  },
					new Function() {Id = "ROLE", Name = "Role",ParentId = "SYSTEM",SortOrder = 1,Status = Status.Active,URL = "/admin/role/index",IconCss = "fa-home"  },
					new Function() {Id = "FUNCTION", Name = "Function",ParentId = "SYSTEM",SortOrder = 2,Status = Status.Active,URL = "/admin/function/index",IconCss = "fa-home"  },
					new Function() {Id = "USER", Name = "User",ParentId = "SYSTEM",SortOrder =3,Status = Status.Active,URL = "/admin/user/index",IconCss = "fa-home"  },
					new Function() {Id = "ACTIVITY", Name = "Activity",ParentId = "SYSTEM",SortOrder = 4,Status = Status.InActive,URL = "/admin/activity/index",IconCss = "fa-home"  },
					new Function() {Id = "ERROR", Name = "Error",ParentId = "SYSTEM",SortOrder = 5,Status = Status.InActive,URL = "/admin/error/index",IconCss = "fa-home"  },
					new Function() {Id = "SETTING", Name = "Configuration",ParentId = "SYSTEM",SortOrder = 6,Status = Status.InActive,URL = "/admin/setting/index",IconCss = "fa-home"  },
					
					new Function() {Id = "CONTENT",Name = "Content",ParentId = null,SortOrder = 3,Status = Status.Active,URL = "/",IconCss = "fa-table"  },
					new Function() {Id = "PAGE",Name = "Page",ParentId = "CONTENT",SortOrder = 2,Status = Status.Active,URL = "/admin/page/index",IconCss = "fa-table"  },
					new Function() {Id = "TYPE",Name = "Danh mục tin",ParentId = "CONTENT",SortOrder =1,Status = Status.Active,URL = "/admin/type/index",IconCss = "fa-table"  },
					new Function() {Id = "CLASSIFIEDS",Name = "Đăng tin",ParentId = "CONTENT",SortOrder = 1,Status = Status.Active,URL = "/admin/classifieds/index",IconCss = "fa-table"  },
					new Function() {Id = "ANNOUNCEMENT",Name = "Announcement",ParentId = "UTILITY",SortOrder = 3,Status = Status.InActive,URL = "/admin/announcement/index",IconCss = "fa-clone"  },
					new Function() {Id = "CONTACT",Name = "Contact",ParentId = "UTILITY",SortOrder = 4,Status = Status.Active,URL = "/admin/contact/index",IconCss = "fa-clone"  },
					new Function() {Id = "SLIDE",Name = "Slide",ParentId = "UTILITY",SortOrder = 5,Status = Status.Active,URL = "/admin/slideshow/index",IconCss = "fa-clone"  },
				});
			}

			if(_context.Footers.Count(x => x.Id == CommonConstants.DefaultFooterId) == 0)
			{
				string content = "Footer";
				_context.Footers.Add(new Footer() {
					Id = CommonConstants.DefaultFooterId,
					Content = content
				});
			}

			
			if(_context.AdvertistmentPages.Count() == 0)
			{
				List<AdvertistmentPage> pages = new List<AdvertistmentPage>()
				{
					new AdvertistmentPage() {Id="home", Name="Home",AdvertistmentPositions = new List<AdvertistmentPosition>(){
						new AdvertistmentPosition(){Id="home-left",Name="Bên trái"}
					} },
					new AdvertistmentPage() {Id="product-cate", Name="Product category" ,
						AdvertistmentPositions = new List<AdvertistmentPosition>(){
						new AdvertistmentPosition(){Id="product-cate-left",Name="Bên trái"}
					}},
					new AdvertistmentPage() {Id="product-detail", Name="Product detail",
						AdvertistmentPositions = new List<AdvertistmentPosition>(){
						new AdvertistmentPosition(){Id="product-detail-left",Name="Bên trái"}
					} },

				};
				_context.AdvertistmentPages.AddRange(pages);
			}


			if(_context.Slides.Count() == 0)
			{
				List<Slide> slides = new List<Slide>()
				{
					new Slide() {Name="Slide 1",Image="/client-side/images/slider/slide-1.jpg",Url="#",DisplayOrder = 0,GroupAlias = "top",Status = Status.Active },
					new Slide() {Name="Slide 2",Image="/client-side/images/slider/slide-2.jpg",Url="#",DisplayOrder = 1,GroupAlias = "top",Status = Status.Active },
					new Slide() {Name="Slide 3",Image="/client-side/images/slider/slide-3.jpg",Url="#",DisplayOrder = 2,GroupAlias = "top",Status = Status.Active },

					new Slide() {Name="Slide 1",Image="/client-side/images/brand1.png",Url="#",DisplayOrder = 1,GroupAlias = "brand",Status = Status.Active },
					new Slide() {Name="Slide 2",Image="/client-side/images/brand2.png",Url="#",DisplayOrder = 2,GroupAlias = "brand",Status = Status.Active },
					new Slide() {Name="Slide 3",Image="/client-side/images/brand3.png",Url="#",DisplayOrder = 3,GroupAlias = "brand",Status = Status.Active },
					new Slide() {Name="Slide 4",Image="/client-side/images/brand4.png",Url="#",DisplayOrder = 4,GroupAlias = "brand",Status = Status.Active },
					new Slide() {Name="Slide 5",Image="/client-side/images/brand5.png",Url="#",DisplayOrder = 5,GroupAlias = "brand",Status = Status.Active },
					new Slide() {Name="Slide 6",Image="/client-side/images/brand6.png",Url="#",DisplayOrder = 6,GroupAlias = "brand",Status = Status.Active },
					new Slide() {Name="Slide 7",Image="/client-side/images/brand7.png",Url="#",DisplayOrder = 7,GroupAlias = "brand",Status = Status.Active },
					new Slide() {Name="Slide 8",Image="/client-side/images/brand8.png",Url="#",DisplayOrder = 8,GroupAlias = "brand",Status = Status.Active },
					new Slide() {Name="Slide 9",Image="/client-side/images/brand9.png",Url="#",DisplayOrder = 9,GroupAlias = "brand",Status = Status.Active },
					new Slide() {Name="Slide 10",Image="/client-side/images/brand10.png",Url="#",DisplayOrder = 10,GroupAlias = "brand",Status = Status.Active },
					new Slide() {Name="Slide 11",Image="/client-side/images/brand11.png",Url="#",DisplayOrder = 11,GroupAlias = "brand",Status = Status.Active },
				};
				_context.Slides.AddRange(slides);
			}
	

			if(!_context.SystemConfigs.Any(x => x.Id == "HomeTitle"))
			{
				_context.SystemConfigs.Add(new SystemConfig() {
					Id = "HomeTitle",
					Name = "Home's title",
					Value1 = "structures Shop home",
					Status = Status.Active
				});
			}
			if(!_context.SystemConfigs.Any(x => x.Id == "HomeMetaKeyword"))
			{
				_context.SystemConfigs.Add(new SystemConfig() {
					Id = "HomeMetaKeyword",
					Name = "Home Keyword",
					Value1 = "shopping, sales",
					Status = Status.Active
				});
			}
			if(!_context.SystemConfigs.Any(x => x.Id == "HomeMetaDescription"))
			{
				_context.SystemConfigs.Add(new SystemConfig() {
					Id = "HomeMetaDescription",
					Name = "Home Description",
					Value1 = "Home structures",
					Status = Status.Active
				});
			}
			await _context.SaveChangesAsync();

		}
	}
}

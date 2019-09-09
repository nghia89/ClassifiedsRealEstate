using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Linq;
using WebAppCore.Data.EF.BuilderExtensions;
using WebAppCore.Data.Entities;
using WebAppCore.Data.Interfaces;

namespace WebAppCore.Data.EF
{
	public class AppDbContext:IdentityDbContext<AppUser,AppRole,Guid>
	{
		public AppDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<SystemConfig> SystemConfigs { get; set; }
		public DbSet<Function> Functions { get; set; }

		public DbSet<AppUser> AppUsers { get; set; }
		public DbSet<AppRole> AppRoles { get; set; }
		public DbSet<Announcement> Announcements { set; get; }
		public DbSet<AnnouncementUser> AnnouncementUsers { set; get; }

		public DbSet<Classifieds> Classifieds { set; get; }
		public DbSet<Entities.Type> Types { set; get; }
		public DbSet<ClassifiedsTag> ClassifiedsTags { set; get; }
		public DbSet<Contact> Contacts { set; get; }
		public DbSet<Feedback> Feedbacks { set; get; }
		public DbSet<Footer> Footers { set; get; }
		public DbSet<Page> Pages { set; get; }
		public DbSet<ImageList> ProductImages { set; get; }

		public DbSet<Slide> Slides { set; get; }
		public DbSet<Tag> Tags { set; get; }

		public DbSet<Permission> Permissions { get; set; }
		public DbSet<AdvertistmentPage> AdvertistmentPages { get; set; }
		public DbSet<Advertistment> Advertistments { get; set; }
		public DbSet<AdvertistmentPosition> AdvertistmentPositions { get; set; }

		public DbSet<Address> Addresses { get; set; }
		public DbSet<ClassifiedsAddress> ClassifiedsAddress { get; set; }
		public DbSet<Ward> Wards { get; set; }
		public DbSet<District> Districts { get; set; }
		public DbSet<City> Citys { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ConfigModelBuilder();
			base.OnModelCreating(builder);
		}

		


		public override int SaveChanges()
		{
			var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);

			foreach(EntityEntry item in modified)
			{
				var changedOrAddedItem = item.Entity as IDateTracking;
				if(changedOrAddedItem != null)
				{
					if(item.State == EntityState.Added)
					{
						changedOrAddedItem.DateCreated = DateTime.Now;
					}
					changedOrAddedItem.DateModified = DateTime.Now;
				}
			}
			return base.SaveChanges();
		}
	}

	public class DesignTimeDbContextFactory:IDesignTimeDbContextFactory<AppDbContext>
	{
		public AppDbContext CreateDbContext(string[] args)
		{
			IConfiguration configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json").Build();
			var builder = new DbContextOptionsBuilder<AppDbContext>();
			var connectionString = configuration.GetConnectionString("DefaultConnection");
			builder.UseSqlServer(connectionString);
			return new AppDbContext(builder.Options);
		}
	}
}
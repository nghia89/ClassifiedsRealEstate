using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using WebAppCore.Data.EF.Configurations;
using WebAppCore.Data.EF.Extensions;
using WebAppCore.Data.Entities;

namespace WebAppCore.Data.EF.BuilderExtensions
{
	public static class BuilderExtensions
	{
		public static void ConfigModelBuilder(this ModelBuilder modelBuilder)
		{

			#region Identity Config

			modelBuilder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims").HasKey(x => x.Id);

			modelBuilder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims")
				.HasKey(x => x.Id);

			modelBuilder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);

			modelBuilder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles")
				.HasKey(x => new { x.RoleId,x.UserId });

			modelBuilder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens")
			   .HasKey(x => new { x.UserId });

			#endregion Identity Config

			modelBuilder.AddConfiguration(new AdvertistmentPositionConfiguration());
			modelBuilder.AddConfiguration(new BlogTagConfiguration());
			modelBuilder.AddConfiguration(new ContactDetailConfiguration());
			modelBuilder.AddConfiguration(new FooterConfiguration());
			modelBuilder.AddConfiguration(new FunctionConfiguration());
			modelBuilder.AddConfiguration(new PageConfiguration());
			modelBuilder.AddConfiguration(new SystemConfigConfiguration());
			modelBuilder.AddConfiguration(new TagConfiguration());
			modelBuilder.AddConfiguration(new AnnouncementConfiguration());
			modelBuilder.AddConfiguration(new AdvertistmentPageConfiguration());

			modelBuilder.Entity<Classifieds>(entity => {
				entity.ToTable(nameof(Classifieds));
				entity.Property(x => x.Title).HasMaxLength(255).IsRequired();
				entity.HasIndex(x => x.Id).IsUnique();
				entity.HasIndex(x => new { x.Title });
			});
			
			modelBuilder.Entity<City>(entity => {
				entity.ToTable(nameof(City));
				entity.Property(x => x.Code).HasMaxLength(200);
				entity.Property(x => x.Name).HasMaxLength(200);
				entity.HasIndex(m => new { m.Code,m.Name });
			});
			modelBuilder.Entity<District>(entity => {
				entity.ToTable(nameof(District));
				entity.Property(x => x.Code).HasMaxLength(200);
				entity.Property(x => x.Name).HasMaxLength(200);
				entity.HasIndex(m => new { m.Code,m.Name });
				entity.HasOne(x => x.City).WithMany(y => y.Districts).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
			});
			modelBuilder.Entity<Ward>(entity => {
				entity.ToTable(nameof(Ward));
				entity.Property(x => x.Code).HasMaxLength(200);
				entity.Property(x => x.Name).HasMaxLength(200);
				entity.HasIndex(m => new { m.Code,m.Name });
				entity.HasOne(x => x.District).WithMany(y => y.Wards).IsRequired(false).OnDelete(DeleteBehavior.Cascade);
			});
		}
	}
}

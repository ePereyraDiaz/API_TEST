

using Actividades.Domain.Models;
using Actividades.Persistence.DB_Context.Configurations;
using Actividades.Persistence.IdentityAuth;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Actividades.Persistence.DB_Context
{
	public class ActivitiesDbContext : IdentityDbContext<ApplicationUser>
	{
		public ActivitiesDbContext(DbContextOptions<ActivitiesDbContext> options) : base(options)
		{

		}

		public ActivitiesDbContext() { }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("DefaultConnection");

			}
		}

		#region DBSets<>
		public DbSet<SERProperty> SERProperty { get; set; }
		public virtual DbSet<SERActivity> SERActivity { get; set; }
		public virtual DbSet<SERSurvey> SERSurvey { get; set; }
		#endregion

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new PropertyConfiguration());

			modelBuilder.ApplyConfiguration(new ActivityConfiguration());

			modelBuilder.ApplyConfiguration(new SurveyConfiguration());

			//OnModelCreatingPartial(modelBuilder);

			base.OnModelCreating(modelBuilder);
		}

		//partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}

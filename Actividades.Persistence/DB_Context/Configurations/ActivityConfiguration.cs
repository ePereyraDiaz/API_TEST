using Actividades.Domain.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Actividades.Persistence.DB_Context.Configurations
{
	public class ActivityConfiguration : IEntityTypeConfiguration<SERActivity>
	{
		public void Configure(EntityTypeBuilder<SERActivity> builder)
		{
			builder.ToTable("Activity");

			builder.HasKey(e => e.Id).HasName("PK_idActivity");

			builder.Property(e => e.Schedule).HasColumnName("schedule").HasColumnType("datetime").IsRequired(true);

			builder.Property(e => e.Title).HasColumnName("title").HasColumnType("varchar").HasMaxLength(255).IsRequired(true);

			builder.Property(e => e.Created_at).HasColumnName("created_at").HasColumnType("datetime").IsRequired(true);

			builder.Property(e => e.Updated_at).HasColumnName("updated_at").HasColumnType("datetime").IsRequired(true);

			builder.Property(e => e.Status).HasColumnName("status").HasColumnType("varchar").HasMaxLength(35).IsRequired(true);

			builder.HasOne(d => d.PropertyNavigation)
				.WithMany(p => p.Activity)
				.HasForeignKey(d => d.property_id)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_property_id");
		}
	}
}

using Actividades.Domain.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Actividades.Persistence.DB_Context.Configurations
{
	public class PropertyConfiguration : IEntityTypeConfiguration<SERProperty>
	{
		public void Configure(EntityTypeBuilder<SERProperty> builder)
		{
			builder.ToTable("Property");

			builder.HasKey(e => e.Id).HasName("PK_idProperty");

			builder.Property(e => e.Title).HasColumnName("title").HasColumnType("varchar").HasMaxLength(255).IsRequired(true);

			builder.Property(e => e.Address).HasColumnName("address").HasColumnType("varchar(max)").IsRequired(true);

			builder.Property(e => e.Description).HasColumnName("description").HasColumnType("varchar(max)").IsRequired(true);

			builder.Property(e => e.Created_at).HasColumnName("created_at").HasColumnType("datetime").IsRequired(true);

			builder.Property(e => e.Updated_at).HasColumnName("updated_at").HasColumnType("datetime").IsRequired(true);

			builder.Property(e => e.Disabled_at).HasColumnName("disabled_at").HasColumnType("datetime");

			builder.Property(e => e.Status).HasColumnName("status").HasColumnType("varchar").HasMaxLength(35).IsRequired(true);

		}
	}
}

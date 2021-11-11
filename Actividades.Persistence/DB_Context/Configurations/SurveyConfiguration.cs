using Actividades.Domain.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Actividades.Persistence.DB_Context.Configurations
{
	public class SurveyConfiguration : IEntityTypeConfiguration<SERSurvey>
	{
		public void Configure(EntityTypeBuilder<SERSurvey> builder)
		{
			builder.ToTable("Survey");

			builder.HasKey(e => e.Id).HasName("PK_idSurvey");

			builder.Property(e => e.Answers).HasColumnName("answers").HasColumnType("varchar(max)").IsRequired(true);

			builder.Property(e => e.Created_at).HasColumnName("created_at").HasColumnType("datetime").IsRequired(true);

			builder.HasOne(d => d.ActivityNavigation)
				.WithMany(p => p.Survey)
				.HasForeignKey(d => d.activity_id)
				.OnDelete(DeleteBehavior.ClientSetNull)
				.HasConstraintName("FK_activity_id");
		}
	}
}

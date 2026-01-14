using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicantTracking.Domain.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApplicantTracking.Infrastructure.Configuration
{
    public class TimelineMap : IEntityTypeConfiguration<Timeline>
    {
        public void Configure(EntityTypeBuilder<Timeline> builder)
        {
            builder.ToTable("timelines", "dbo");

            builder.HasKey(t => t.IdTimeline);

            builder.Property(t => t.IdTimeline)
                .HasColumnName("IdTimeline")
                .ValueGeneratedOnAdd();

            builder.Property(t => t.IdTimelineType)
                .HasColumnType("tinyint")
                .IsRequired();

            builder.Property(t => t.IdAggregateRoot)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(t => t.OldData)
                .HasColumnType("varchar(max)")
                .IsRequired(false);

            builder.Property(t => t.NewData)
                .HasColumnType("varchar(max)")
                .IsRequired(false);

            builder.Property(t => t.CreatedAt)
                .HasColumnType("datetime")
                .IsRequired();
        }
    }
}

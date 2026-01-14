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
    public class CandidateMap : IEntityTypeConfiguration<Candidate>
    {
        public void Configure(EntityTypeBuilder<Candidate> builder)
        {
            builder.ToTable("candidates", "dbo");

            builder.HasKey(c => c.IdCandidate);

            builder.Property(c => c.IdCandidate)
                .HasColumnName("IdCandidate")
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Name)
                .HasColumnType("varchar(80)")
                .IsRequired();

            builder.Property(c => c.Surname)
                .HasColumnType("varchar(150)")
                .IsRequired();

            builder.Property(c => c.Birthdate)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.Email)
                .HasColumnType("varchar(250)")
                .IsRequired();

            builder.Property(c => c.CreatedAt)
                .HasColumnType("datetime")
                .IsRequired();

            builder.Property(c => c.LastUpdatedAt)
                .HasColumnType("datetime")
                .IsRequired(false);
        }
    }
}

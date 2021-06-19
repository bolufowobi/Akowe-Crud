using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Data.EntityConfiguratons
{
    public class AcademicCredentialConfiguration : IEntityTypeConfiguration<AcademicCredential>
    {
        public void Configure(EntityTypeBuilder<AcademicCredential> builder)
        {
            builder.Property(p => p.Firstname).IsRequired().HasMaxLength(500);
            builder.Property(p => p.Lastname).IsRequired().HasMaxLength(500);
            builder.Property(p => p.GPA).HasColumnType("decimal(5,2)")
                .IsRequired();
            builder.Property(p => p.Email).IsRequired().HasMaxLength(500);
            builder.Property(p => p.TranscriptUrl).HasMaxLength(200);
        }
    }
}

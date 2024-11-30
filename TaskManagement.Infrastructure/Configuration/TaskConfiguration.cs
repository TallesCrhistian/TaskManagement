using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Configuration
{
        public class TaskEntityConfiguration : IEntityTypeConfiguration<TaskEntity>
        {
            public void Configure(EntityTypeBuilder<TaskEntity> entityTypeBuilder)
            {
                entityTypeBuilder.ToTable("task");

                entityTypeBuilder.HasKey(x => x.Id);

                entityTypeBuilder.Property(e => e.Id)
                    .HasConversion<Guid>()
                    .HasColumnName("id");

                entityTypeBuilder.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(400)
                    .IsRequired(false);

                entityTypeBuilder.Property(e => e.Title)
                   .HasColumnName("title")
                   .HasMaxLength(100)
                   .IsRequired();

                entityTypeBuilder.Property(e => e.Status)
                   .HasColumnName("status")
                   .IsRequired();

                entityTypeBuilder.Property(e => e.CreatedAt)
                   .HasColumnName("created_at")
                   .IsRequired();

                entityTypeBuilder.Property(e => e.UpdatedAt)
                   .HasColumnName("updated_at")
                   .IsRequired(false);
            }
        }
    }
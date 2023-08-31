using Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.EntitiesConfigurations;

public class LogConfiguration : IEntityTypeConfiguration<Log>
{
    public void Configure(EntityTypeBuilder<Log> builder)
    {
        builder.ToTable("log");

        builder.HasKey(x => x.Id)
            .HasName("PK_log");

        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(x => x.EventId)
            .HasColumnName("event_id");

        builder.Property(x => x.LogLevel)
            .HasColumnName("log_level")
            .HasMaxLength(30);

        builder.Property(x => x.Message)
            .HasColumnName("message")
            .HasColumnType("text");

        builder.Property(x => x.Exception)
            .HasColumnName("exception")
            .HasColumnType("text");

        builder.Property(x => x.Path)
            .HasColumnName("path")
            .HasMaxLength(250);

        builder.Property(x => x.Data)
            .HasColumnName("data")
            .HasColumnType("text");

        builder.Property(x => x.DataType)
            .HasColumnName("data_type")
            .HasMaxLength(250);

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP()");
    }
}

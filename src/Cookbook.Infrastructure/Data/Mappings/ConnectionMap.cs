using Cookbook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cookbook.Infrastructure.Data.Mappings;

public class ConnectionMap : IEntityTypeConfiguration<Connection>
{
    public void Configure(EntityTypeBuilder<Connection> builder)
    {
        builder.ToTable("Connections");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedOn)
            .IsRequired()
            .HasColumnName("CreatedOn")
            .HasColumnType("timestamptz");

        builder.HasOne(x => x.User)
               .WithMany()
               .HasConstraintName("FK_Connection_User");

        builder.HasOne(x => x.ConnectedWithUser)
            .WithMany()
            .HasConstraintName("FK_Connection_ConnectedWithUsers");
    }
}

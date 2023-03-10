using Cookbook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cookbook.Infrastructure.Data.Mappings;

public class CodeMap : IEntityTypeConfiguration<Code>
{
    public void Configure(EntityTypeBuilder<Code> builder)
    {
        builder.ToTable("Codes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedOn)
            .IsRequired()
            .HasColumnName("CreatedOn")
            .HasColumnType("timestamptz");

        builder.Property(x => x.Value)
            .IsRequired()
            .HasColumnName("Value")
            .HasColumnType("VARCHAR")
            .HasMaxLength(2000);

        builder.HasOne(x => x.User)
            .WithOne(x => x.Code)
            .HasConstraintName("FK_Code_User_Id");
    }
}

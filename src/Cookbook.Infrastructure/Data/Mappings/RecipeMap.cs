using Cookbook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cookbook.Infrastructure.Data.Mappings;

internal class RecipeMap : IEntityTypeConfiguration<Recipe>
{
    public void Configure(EntityTypeBuilder<Recipe> builder)
    {
        builder.ToTable("Recipes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedOn)
            .IsRequired()
            .HasColumnName("CreatedOn")
            .HasColumnType("timestamptz");

        builder.Property(x => x.Title)
            .IsRequired()
            .HasColumnName("Title")
            .HasColumnType("VARCHAR")
            .HasMaxLength(100);

        builder.Property(x => x.PreparationMode)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasColumnName("PreparationMode")
            .HasMaxLength(160);

        builder.Property(x => x.Category)
            .HasColumnName("Category")
            .HasColumnType("INT")
            .IsRequired();
    }
}

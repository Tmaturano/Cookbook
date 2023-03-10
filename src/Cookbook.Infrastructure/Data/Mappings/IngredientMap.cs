using Cookbook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cookbook.Infrastructure.Data.Mappings;

internal class IngredientMap : IEntityTypeConfiguration<Ingredient>
{
    public void Configure(EntityTypeBuilder<Ingredient> builder)
    {
        builder.ToTable("Ingredients");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.CreatedOn)
            .IsRequired()
            .HasColumnName("CreatedOn")
            .HasColumnType("timestamptz");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasColumnName("Name")
            .HasColumnType("VARCHAR")
            .HasMaxLength(100);

        builder.Property(x => x.Quantity)
            .IsRequired()
            .HasColumnType("VARCHAR")
            .HasColumnName("Quantity")
            .HasMaxLength(160);

        builder.HasOne(x => x.Recipe)
            .WithMany(x => x.Ingredients)
            .HasConstraintName("FK_Ingredient_Recipe")
            .OnDelete(DeleteBehavior.Cascade);
    }
}

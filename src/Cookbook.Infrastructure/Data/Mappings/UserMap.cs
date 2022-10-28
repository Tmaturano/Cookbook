using Cookbook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cookbook.Infrastructure.Data.Mappings
{
    internal class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.CreatedOn)
                .IsRequired()
                .HasColumnName("CreatedOn")
                .HasColumnType("timestamp");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("VARCHAR")
                .HasColumnName("Name")
                .HasMaxLength(100);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnType("VARCHAR")
                .HasColumnName("Email")
                .HasMaxLength(160);
     
            builder.Property(x => x.PasswordHash)
                .HasColumnName("PasswordHash")
                .IsRequired();

            builder.Property(x => x.Phone)
                .HasColumnType("VARCHAR")
                .HasColumnName("Phone")
                .HasMaxLength(14)
                .IsRequired();

            builder.HasIndex(x => x.Email, "IX_User_Email")
                .IsUnique();
        }
    }
}

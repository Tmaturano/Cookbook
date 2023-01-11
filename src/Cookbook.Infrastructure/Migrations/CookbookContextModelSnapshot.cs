﻿// <auto-generated />
using System;
using Cookbook.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Cookbook.Infrastructure.Migrations
{
    [DbContext(typeof(CookbookContext))]
    partial class CookbookContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Cookbook.Domain.Entities.Ingredient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamptz")
                        .HasColumnName("CreatedOn");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Name");

                    b.Property<string>("Quantity")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Quantity");

                    b.Property<Guid?>("RecipeId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("Ingredients", (string)null);
                });

            modelBuilder.Entity("Cookbook.Domain.Entities.Recipe", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Category")
                        .HasColumnType("INT")
                        .HasColumnName("Category");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamptz")
                        .HasColumnName("CreatedOn");

                    b.Property<Guid?>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<string>("PreparationMode")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("PreparationMode");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Recipes", (string)null);
                });

            modelBuilder.Entity("Cookbook.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamptz")
                        .HasColumnName("CreatedOn");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(160)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("PasswordHash");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(14)
                        .HasColumnType("VARCHAR")
                        .HasColumnName("Phone");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "Email" }, "IX_User_Email")
                        .IsUnique();

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("Cookbook.Domain.Entities.Ingredient", b =>
                {
                    b.HasOne("Cookbook.Domain.Entities.Recipe", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_Ingredient_Recipe");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Cookbook.Domain.Entities.Recipe", b =>
                {
                    b.HasOne("Cookbook.Domain.Entities.User", "Owner")
                        .WithMany("Recipes")
                        .HasForeignKey("OwnerId")
                        .HasConstraintName("FK_Recipe_User");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Cookbook.Domain.Entities.Recipe", b =>
                {
                    b.Navigation("Ingredients");
                });

            modelBuilder.Entity("Cookbook.Domain.Entities.User", b =>
                {
                    b.Navigation("Recipes");
                });
#pragma warning restore 612, 618
        }
    }
}

﻿// <auto-generated />
using System;
using FitLife.DB.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FitLife.DB.Migrations.Diet
{
    [DbContext(typeof(DietContext))]
    partial class DietContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FitLife.DB.Models.Authentication.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(150)");

                    b.Property<bool?>("IsDisabled")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("AppUser");
                });

            modelBuilder.Entity("FitLife.DB.Models.Diet.UserPeriodicDiet", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<decimal>("Calories")
                        .HasColumnType("decimal(4,1)");

                    b.Property<decimal>("CarbsGrams")
                        .HasColumnType("decimal(4,1)");

                    b.Property<decimal>("FatsGrams")
                        .HasColumnType("decimal(4,1)");

                    b.Property<DateTime>("PeriodEnd")
                        .HasColumnType("date");

                    b.Property<DateTime>("PeriodStart")
                        .HasColumnType("date");

                    b.Property<decimal>("ProteinsGrams")
                        .HasColumnType("decimal(4,1)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserPeriodicDiet", (string)null);
                });

            modelBuilder.Entity("FitLife.DB.Models.Food.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("FitLife.DB.Models.Food.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("VARCHAR(64)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Meal");
                });

            modelBuilder.Entity("FitLife.DB.Models.Food.MealProduct", b =>
                {
                    b.Property<int>("MealProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MealProductId"), 1L, 1);

                    b.Property<int>("Grams")
                        .HasColumnType("int");

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("MealProductId");

                    b.HasIndex("MealId");

                    b.HasIndex("ProductId");

                    b.ToTable("MealProduct");
                });

            modelBuilder.Entity("FitLife.DB.Models.Food.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("Calories")
                        .HasColumnType("decimal(4,1)");

                    b.Property<decimal>("CarbsGrams")
                        .HasColumnType("decimal(4,1)");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<decimal>("FatsGrams")
                        .HasColumnType("decimal(4,1)");

                    b.Property<string>("Name")
                        .HasColumnType("VARCHAR(32)");

                    b.Property<decimal>("ProteinsGrams")
                        .HasColumnType("decimal(4,1)");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("FitLife.DB.Models.Food.UserMeal", b =>
                {
                    b.Property<int>("UserMealId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserMealId"), 1L, 1);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("ConsumedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserMealId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("MealId");

                    b.HasIndex("UserId");

                    b.ToTable("UserMeal");
                });

            modelBuilder.Entity("FitLife.DB.Models.Diet.UserPeriodicDiet", b =>
                {
                    b.HasOne("FitLife.DB.Models.Authentication.AppUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FitLife.DB.Models.Food.Meal", b =>
                {
                    b.HasOne("FitLife.DB.Models.Food.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("FitLife.DB.Models.Food.MealProduct", b =>
                {
                    b.HasOne("FitLife.DB.Models.Food.Meal", "Meal")
                        .WithMany("MealProducts")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitLife.DB.Models.Food.Product", "Product")
                        .WithMany("MealProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Meal");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("FitLife.DB.Models.Food.UserMeal", b =>
                {
                    b.HasOne("FitLife.DB.Models.Food.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitLife.DB.Models.Food.Meal", "Meal")
                        .WithMany("UserMeals")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitLife.DB.Models.Authentication.AppUser", "User")
                        .WithMany("UserMeals")
                        .HasForeignKey("UserId");

                    b.Navigation("Category");

                    b.Navigation("Meal");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FitLife.DB.Models.Authentication.AppUser", b =>
                {
                    b.Navigation("UserMeals");
                });

            modelBuilder.Entity("FitLife.DB.Models.Food.Meal", b =>
                {
                    b.Navigation("MealProducts");

                    b.Navigation("UserMeals");
                });

            modelBuilder.Entity("FitLife.DB.Models.Food.Product", b =>
                {
                    b.Navigation("MealProducts");
                });
#pragma warning restore 612, 618
        }
    }
}

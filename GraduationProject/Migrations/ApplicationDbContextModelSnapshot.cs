﻿// <auto-generated />
using GraduationProject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GraduationProject.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GraduationProject.Models.Beverage", b =>
                {
                    b.Property<int>("BeverageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BeverageId"));

                    b.Property<bool>("Alcohol")
                        .HasColumnType("bit");

                    b.Property<string>("Glass")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Instruction")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("nvarchar(1500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Tag")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Video")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BeverageId");

                    b.ToTable("Beverages");

                    b.HasData(
                        new
                        {
                            BeverageId = 1,
                            Alcohol = true,
                            Glass = "Martini Glass",
                            Image = "http://potatomargarita.com",
                            Instruction = "Shake it like a polaroid picture",
                            Name = "Potato Margarita",
                            Tag = "ordinary"
                        },
                        new
                        {
                            BeverageId = 2,
                            Alcohol = true,
                            Glass = "Thumbler",
                            Image = "http://tomatomartini.com",
                            Instruction = "Stir it up",
                            Name = "Tomato Martini",
                            Tag = "cocktail"
                        },
                        new
                        {
                            BeverageId = 3,
                            Alcohol = false,
                            Glass = "Long glass",
                            Image = "http://brocolioldfashined.com",
                            Instruction = "On the grind",
                            Name = "Brocoli Old Fashioned",
                            Tag = "ordinary"
                        });
                });

            modelBuilder.Entity("GraduationProject.Models.BeverageIngredient", b =>
                {
                    b.Property<int>("BeverageIngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BeverageIngredientId"));

                    b.Property<int>("BeverageId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<string>("Measurment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BeverageIngredientId");

                    b.HasIndex("BeverageId");

                    b.HasIndex("IngredientId");

                    b.ToTable("BeverageIngredients");

                    b.HasData(
                        new
                        {
                            BeverageIngredientId = 1,
                            BeverageId = 1,
                            IngredientId = 1,
                            Measurment = "60ml"
                        },
                        new
                        {
                            BeverageIngredientId = 2,
                            BeverageId = 1,
                            IngredientId = 2,
                            Measurment = "One Slice"
                        },
                        new
                        {
                            BeverageIngredientId = 3,
                            BeverageId = 1,
                            IngredientId = 3,
                            Measurment = "35ml"
                        });
                });

            modelBuilder.Entity("GraduationProject.Models.Favorite", b =>
                {
                    b.Property<int>("FavoriteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FavoriteId"));

                    b.Property<int>("BeverageId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FavoriteId");

                    b.HasIndex("BeverageId");

                    b.HasIndex("UserId");

                    b.ToTable("Favorites");

                    b.HasData(
                        new
                        {
                            FavoriteId = 1,
                            BeverageId = 1,
                            UserId = 1
                        },
                        new
                        {
                            FavoriteId = 2,
                            BeverageId = 2,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("GraduationProject.Models.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredientId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("nvarchar(1500)");

                    b.Property<string>("Image")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("IngredientId");

                    b.ToTable("Ingredients");

                    b.HasData(
                        new
                        {
                            IngredientId = 1,
                            Description = "Great vegetable, quite bitter",
                            Image = "http://brocoli.com",
                            Name = "Brocoli Liqueur"
                        },
                        new
                        {
                            IngredientId = 2,
                            Description = "Saved nations from famine",
                            Image = "http://potato.com",
                            Name = "Potato"
                        },
                        new
                        {
                            IngredientId = 3,
                            Description = "The italian berry",
                            Image = "http://tomato.com",
                            Name = "Tomato extract"
                        });
                });

            modelBuilder.Entity("GraduationProject.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            Email = "kickass@gmail.com",
                            Password = "NinjaKick",
                            UserName = "ChuckNorris"
                        },
                        new
                        {
                            UserId = 2,
                            Email = "iiiiiijjjaaa@hotmail.com",
                            Password = "RoundHouseKick",
                            UserName = "BruceLee"
                        });
                });

            modelBuilder.Entity("GraduationProject.Models.BeverageIngredient", b =>
                {
                    b.HasOne("GraduationProject.Models.Beverage", "Beverage")
                        .WithMany("BeverageIngredients")
                        .HasForeignKey("BeverageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.Ingredient", "Ingredient")
                        .WithMany("BeverageIngredients")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beverage");

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("GraduationProject.Models.Favorite", b =>
                {
                    b.HasOne("GraduationProject.Models.Beverage", "Beverage")
                        .WithMany("Favorites")
                        .HasForeignKey("BeverageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GraduationProject.Models.User", "User")
                        .WithMany("Favorites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beverage");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GraduationProject.Models.Beverage", b =>
                {
                    b.Navigation("BeverageIngredients");

                    b.Navigation("Favorites");
                });

            modelBuilder.Entity("GraduationProject.Models.Ingredient", b =>
                {
                    b.Navigation("BeverageIngredients");
                });

            modelBuilder.Entity("GraduationProject.Models.User", b =>
                {
                    b.Navigation("Favorites");
                });
#pragma warning restore 612, 618
        }
    }
}

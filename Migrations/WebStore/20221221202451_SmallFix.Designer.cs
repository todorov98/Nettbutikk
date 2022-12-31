﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Nettbutikk.Data;

namespace Nettbutikk.Migrations
{
    [DbContext(typeof(WebStoreContext))]
    [Migration("20221221202451_SmallFix")]
    partial class SmallFix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Nettbutikk.Models.CancelOrderConfirmation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("OrderCancelled")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("CancelOrderConfirmations");
                });

            modelBuilder.Entity("Nettbutikk.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("DateFulfilled")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatePlaced")
                        .HasColumnType("datetime2");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<string>("Stage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Nettbutikk.Models.OrderReceipt", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.ToTable("Receipts");
                });

            modelBuilder.Entity("Nettbutikk.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<string>("Currency")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("OrderReceiptId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("OrderReceiptId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("3ad3e476-0d70-4c5a-9ddd-ff68ebbb9d8e"),
                            Category = "Football",
                            Count = 9,
                            Currency = "EUR",
                            Description = "A high quality football manufactured for the UEFA Champions Leage Final 2022/2023 season.",
                            Name = "UEFA Champions Leage 22/23 Final Original Edition",
                            Price = 102.98999999999999
                        },
                        new
                        {
                            Id = new Guid("22bd0a0d-e704-42a8-865e-d58b58f118d2"),
                            Category = "DrinkingBottle",
                            Count = 8,
                            Currency = "EUR",
                            Description = "High quality drinking bottlefrom Puma.",
                            Name = "Puma X2 Bottle",
                            Price = 22.989999999999998
                        },
                        new
                        {
                            Id = new Guid("00d856ce-cb1b-4811-87e5-c3403ee98b85"),
                            Category = "Sweater",
                            Count = 7,
                            Currency = "EUR",
                            Description = "A highquality breathable sweater produced by Nike. Works well for most physical activity.",
                            Name = "Nike Sweater XZ21 Breather Edition",
                            Price = 45.990000000000002
                        },
                        new
                        {
                            Id = new Guid("a21a9091-a965-439d-9ae9-39f1dfbd575e"),
                            Category = "FootballBoots",
                            Count = 7,
                            Currency = "EUR",
                            Description = "High quality football boots from Nike with modern ACC control that provides great control and first touch during all weatherconditions.",
                            Name = "Nike Hypervenom Phantom ACC",
                            Price = 249.49000000000001
                        },
                        new
                        {
                            Id = new Guid("a35dfefc-e86f-49cc-a30e-fe3abf2d7a02"),
                            Category = "Bag",
                            Count = 7,
                            Currency = "EUR",
                            Description = "Completely new and solid bag fromPuma.",
                            Name = "Puma T23 Bag",
                            Price = 75.390000000000001
                        },
                        new
                        {
                            Id = new Guid("f76aeef4-f729-4dc5-9ca7-73b29f5e0725"),
                            Category = "MountainBoots",
                            Count = 8,
                            Currency = "EUR",
                            Description = "High quality mountain boots from Goretex. Provides great comfort and warmth even during the harshest conditions.",
                            Name = "Goretex Z34 Climber Boots",
                            Price = 167.59
                        },
                        new
                        {
                            Id = new Guid("108d33ae-64d1-4895-8e13-b370b399dbe4"),
                            Category = "SportsPants",
                            Count = 11,
                            Currency = "EUR",
                            Description = "Nice and comfortable sports pants by Adidas.",
                            Name = "Adidas F99 Pants Long",
                            Price = 34.990000000000002
                        });
                });

            modelBuilder.Entity("Nettbutikk.Models.ProductOrderRelation", b =>
                {
                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ProductId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("ProductOrderRelations");
                });

            modelBuilder.Entity("Nettbutikk.Models.UserEntity", b =>
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

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

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

                    b.ToTable("UserEntity");
                });

            modelBuilder.Entity("Nettbutikk.Models.CancelOrderConfirmation", b =>
                {
                    b.HasOne("Nettbutikk.Models.Order", "Order")
                        .WithMany("CancelOrderConfirmations")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Nettbutikk.Models.Order", b =>
                {
                    b.HasOne("Nettbutikk.Models.UserEntity", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Nettbutikk.Models.OrderReceipt", b =>
                {
                    b.HasOne("Nettbutikk.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Nettbutikk.Models.Product", b =>
                {
                    b.HasOne("Nettbutikk.Models.OrderReceipt", null)
                        .WithMany("OrderedProducts")
                        .HasForeignKey("OrderReceiptId");
                });

            modelBuilder.Entity("Nettbutikk.Models.ProductOrderRelation", b =>
                {
                    b.HasOne("Nettbutikk.Models.Order", "Order")
                        .WithMany("ProductOrderRelations")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Nettbutikk.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Nettbutikk.Models.Order", b =>
                {
                    b.Navigation("CancelOrderConfirmations");

                    b.Navigation("ProductOrderRelations");
                });

            modelBuilder.Entity("Nettbutikk.Models.OrderReceipt", b =>
                {
                    b.Navigation("OrderedProducts");
                });
#pragma warning restore 612, 618
        }
    }
}

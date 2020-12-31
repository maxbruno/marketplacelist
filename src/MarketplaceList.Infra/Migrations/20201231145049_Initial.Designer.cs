﻿// <auto-generated />
using System;
using MarketplaceList.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MarketplaceList.Infra.Migrations
{
    [DbContext(typeof(EntityContext))]
    [Migration("20201231145049_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("MarketplaceList.Domain.Models.Item", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("DATETIME2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("VARCHAR(1024)");

                    b.Property<int>("Qtd")
                        .HasColumnType("Int32");

                    b.Property<Guid>("ShoppingListId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ShoppingListId");

                    b.ToTable("Item", "dbo");
                });

            modelBuilder.Entity("MarketplaceList.Domain.Models.ShoppingList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateAt")
                        .HasColumnType("DATETIME2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("VARCHAR(1024)");

                    b.HasKey("Id");

                    b.ToTable("ShoppingList", "dbo");

                    b.HasData(
                        new
                        {
                            Id = new Guid("43d450a4-a859-4fa6-9816-2d43836a1a13"),
                            CreateAt = new DateTime(2020, 12, 31, 11, 50, 49, 100, DateTimeKind.Local).AddTicks(3408),
                            Name = "compras para o churrasco"
                        });
                });

            modelBuilder.Entity("MarketplaceList.Domain.Models.Item", b =>
                {
                    b.HasOne("MarketplaceList.Domain.Models.ShoppingList", "ShoppingList")
                        .WithMany("Itens")
                        .HasForeignKey("ShoppingListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ShoppingList");
                });

            modelBuilder.Entity("MarketplaceList.Domain.Models.ShoppingList", b =>
                {
                    b.Navigation("Itens");
                });
#pragma warning restore 612, 618
        }
    }
}

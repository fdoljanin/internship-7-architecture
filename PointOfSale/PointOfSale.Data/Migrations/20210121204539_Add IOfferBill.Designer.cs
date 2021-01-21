﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PointOfSale.Data.Entities;

namespace PointOfSale.Data.Migrations
{
    [DbContext(typeof(PointOfSaleDbContext))]
    [Migration("20210121204539_Add IOfferBill")]
    partial class AddIOfferBill
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("PointOfSale.Data.Entities.Models.ArticleBill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BillId")
                        .HasColumnType("int");

                    b.Property<int>("OfferId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.HasIndex("OfferId");

                    b.ToTable("ArticleBills");
                });

            modelBuilder.Entity("PointOfSale.Data.Entities.Models.Bill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("Cancelled")
                        .HasColumnType("bit");

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Bills");
                });

            modelBuilder.Entity("PointOfSale.Data.Entities.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("PointOfSale.Data.Entities.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pin")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("PointOfSale.Data.Entities.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pin")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("WorkEnd")
                        .HasColumnType("int");

                    b.Property<int>("WorkStart")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("PointOfSale.Data.Entities.Models.Offer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("PointOfSale.Data.Entities.Models.OfferCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("OfferId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("OfferId");

                    b.ToTable("OfferCategories");
                });

            modelBuilder.Entity("PointOfSale.Data.Entities.Models.ServiceBill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("BillId")
                        .HasColumnType("int");

                    b.Property<int>("Duration")
                        .HasColumnType("int");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<int>("OfferId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("OfferId");

                    b.ToTable("ServiceBills");
                });

            modelBuilder.Entity("PointOfSale.Data.Entities.Models.SubscriptionBill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("BillId")
                        .HasColumnType("int");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<int>("OfferId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("BillId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("OfferId");

                    b.ToTable("SubscriptionBills");
                });

            modelBuilder.Entity("PointOfSale.Data.Entities.Models.ArticleBill", b =>
                {
                    b.HasOne("PointOfSale.Data.Entities.Models.Bill", "Bill")
                        .WithMany("ArticleBills")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PointOfSale.Data.Entities.Models.Offer", "Offer")
                        .WithMany("ArticleBills")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Offer");
                });

            modelBuilder.Entity("PointOfSale.Data.Entities.Models.OfferCategory", b =>
                {
                    b.HasOne("PointOfSale.Data.Entities.Models.Category", "Category")
                        .WithMany("OfferCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PointOfSale.Data.Entities.Models.Offer", "Offer")
                        .WithMany("OfferCategories")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Offer");
                });

            modelBuilder.Entity("PointOfSale.Data.Entities.Models.ServiceBill", b =>
                {
                    b.HasOne("PointOfSale.Data.Entities.Models.Bill", "Bill")
                        .WithMany("ServiceBills")
                        .HasForeignKey("BillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PointOfSale.Data.Entities.Models.Employee", "Employee")
                        .WithMany("ServiceBills")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PointOfSale.Data.Entities.Models.Offer", "Offer")
                        .WithMany("ServiceBills")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Employee");

                    b.Navigation("Offer");
                });

            modelBuilder.Entity("PointOfSale.Data.Entities.Models.SubscriptionBill", b =>
                {
                    b.HasOne("PointOfSale.Data.Entities.Models.Bill", "Bill")
                        .WithMany("SubscriptionBills")
                        .HasForeignKey("BillId");

                    b.HasOne("PointOfSale.Data.Entities.Models.Customer", "Customer")
                        .WithMany("SubscriptionBills")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PointOfSale.Data.Entities.Models.Offer", "Offer")
                        .WithMany("SubscriptionBills")
                        .HasForeignKey("OfferId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Customer");

                    b.Navigation("Offer");
                });

            modelBuilder.Entity("PointOfSale.Data.Entities.Models.Bill", b =>
                {
                    b.Navigation("ArticleBills");

                    b.Navigation("ServiceBills");

                    b.Navigation("SubscriptionBills");
                });

            modelBuilder.Entity("PointOfSale.Data.Entities.Models.Category", b =>
                {
                    b.Navigation("OfferCategories");
                });

            modelBuilder.Entity("PointOfSale.Data.Entities.Models.Customer", b =>
                {
                    b.Navigation("SubscriptionBills");
                });

            modelBuilder.Entity("PointOfSale.Data.Entities.Models.Employee", b =>
                {
                    b.Navigation("ServiceBills");
                });

            modelBuilder.Entity("PointOfSale.Data.Entities.Models.Offer", b =>
                {
                    b.Navigation("ArticleBills");

                    b.Navigation("OfferCategories");

                    b.Navigation("ServiceBills");

                    b.Navigation("SubscriptionBills");
                });
#pragma warning restore 612, 618
        }
    }
}

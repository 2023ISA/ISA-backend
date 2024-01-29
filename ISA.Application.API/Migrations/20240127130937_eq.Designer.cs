﻿// <auto-generated />
using System;
using ISA.Core.Infrastructure.Persistence.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ISA.Application.API.Migrations
{
    [DbContext(typeof(IsaDbContext))]
    [Migration("20240127130937_eq")]
    partial class eq
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ISA.Core.Domain.Entities.Company.Appointment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("AlreadyTaken")
                        .HasColumnType("boolean");

                    b.Property<Guid>("CompanyAdminUserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("EndingDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("StartingDateTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CompanyAdminUserId");

                    b.HasIndex("CompanyId");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.Company.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uuid");

                    b.Property<double?>("AverageGrade")
                        .HasColumnType("double precision");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<TimeOnly>("EndWorkingHour")
                        .HasColumnType("time without time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<TimeOnly>("StartingWorkingHour")
                        .HasColumnType("time without time zone");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.Company.Equipment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Equipments");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.Delivery.Contract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DeliveryDate")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.Delivery.ContractEquipment", b =>
                {
                    b.Property<Guid>("ContractId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EquipmentId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("ContractId", "EquipmentId");

                    b.HasIndex("EquipmentId");

                    b.ToTable("ContractEquipment");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.LoyaltyProgram.LoyaltyProgram", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CategoryDiscounts")
                        .HasColumnType("integer");

                    b.Property<int>("MaxCategoryThresholds")
                        .HasColumnType("integer");

                    b.Property<int>("MaxPenaltyPoints")
                        .HasColumnType("integer");

                    b.Property<int>("MinCategoryThresholds")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("NewPoints")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("LoyaltyPrograms");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.Reservation.Reservation", b =>
                {
                    b.Property<Guid>("AppointmentId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CustomerUserId")
                        .HasColumnType("uuid");

                    b.Property<int>("State")
                        .HasColumnType("integer");

                    b.HasKey("AppointmentId");

                    b.HasIndex("CustomerUserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.Reservation.ReservationEquipment", b =>
                {
                    b.Property<Guid>("ReservationId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("EquipmentId")
                        .HasColumnType("uuid");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("ReservationId", "EquipmentId");

                    b.HasIndex("EquipmentId");

                    b.ToTable("ReservationEquipment");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.User.Address", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.User.CompanyAdmin", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.HasKey("UserId");

                    b.HasIndex("CompanyId");

                    b.ToTable("CompanyAdmins");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.User.Customer", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<string>("CompanyInfo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("LoyaltyProgramId")
                        .HasColumnType("uuid");

                    b.Property<int?>("PenaltyPoints")
                        .HasColumnType("integer");

                    b.Property<int?>("Points")
                        .HasColumnType("integer");

                    b.Property<string>("Profession")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.HasIndex("LoyaltyProgramId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.User.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddressId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.Company.Appointment", b =>
                {
                    b.HasOne("ISA.Core.Domain.Entities.User.CompanyAdmin", "CompanyAdmin")
                        .WithMany()
                        .HasForeignKey("CompanyAdminUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ISA.Core.Domain.Entities.Company.Company", "Company")
                        .WithMany("Appointments")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("CompanyAdmin");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.Company.Company", b =>
                {
                    b.HasOne("ISA.Core.Domain.Entities.User.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.Company.Equipment", b =>
                {
                    b.HasOne("ISA.Core.Domain.Entities.Company.Company", "Company")
                        .WithMany("Equipment")
                        .HasForeignKey("CompanyId");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.Delivery.Contract", b =>
                {
                    b.HasOne("ISA.Core.Domain.Entities.Company.Company", "Company")
                        .WithMany()
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.Delivery.ContractEquipment", b =>
                {
                    b.HasOne("ISA.Core.Domain.Entities.Delivery.Contract", "Contract")
                        .WithMany("Equipments")
                        .HasForeignKey("ContractId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ISA.Core.Domain.Entities.Company.Equipment", "Equipment")
                        .WithMany()
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Contract");

                    b.Navigation("Equipment");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.Reservation.Reservation", b =>
                {
                    b.HasOne("ISA.Core.Domain.Entities.Company.Appointment", "Appointment")
                        .WithMany()
                        .HasForeignKey("AppointmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ISA.Core.Domain.Entities.User.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("CustomerUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Appointment");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.Reservation.ReservationEquipment", b =>
                {
                    b.HasOne("ISA.Core.Domain.Entities.Company.Equipment", "Equipment")
                        .WithMany()
                        .HasForeignKey("EquipmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ISA.Core.Domain.Entities.Reservation.Reservation", "Reservation")
                        .WithMany("Equipments")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Equipment");

                    b.Navigation("Reservation");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.User.CompanyAdmin", b =>
                {
                    b.HasOne("ISA.Core.Domain.Entities.Company.Company", "Company")
                        .WithMany("Admins")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ISA.Core.Domain.Entities.User.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Company");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.User.Customer", b =>
                {
                    b.HasOne("ISA.Core.Domain.Entities.LoyaltyProgram.LoyaltyProgram", "LoyaltyProgram")
                        .WithMany()
                        .HasForeignKey("LoyaltyProgramId");

                    b.HasOne("ISA.Core.Domain.Entities.User.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoyaltyProgram");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.User.User", b =>
                {
                    b.HasOne("ISA.Core.Domain.Entities.User.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.Company.Company", b =>
                {
                    b.Navigation("Admins");

                    b.Navigation("Appointments");

                    b.Navigation("Equipment");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.Delivery.Contract", b =>
                {
                    b.Navigation("Equipments");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.Reservation.Reservation", b =>
                {
                    b.Navigation("Equipments");
                });
#pragma warning restore 612, 618
        }
    }
}

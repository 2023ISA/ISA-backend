﻿// <auto-generated />
using System;
using ISA.Core.Infrastructure.Persistence.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ISA.Application.API.Migrations
{
    [DbContext(typeof(IsaDbContext))]
    partial class IsaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("AdminFirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("AdminId")
                        .HasColumnType("uuid");

                    b.Property<string>("AdminLastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Duration")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Appointment");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.Company.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AddresId")
                        .HasColumnType("uuid");

                    b.Property<double?>("AverageGrade")
                        .HasColumnType("double precision");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("EndWorkingHour")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("StartinWorkingHour")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("AddresId")
                        .IsUnique();

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.Company.Equipment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Equipments");
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
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("UserId");

                    b.ToTable("CompanyAdmins");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.User.Customer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
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

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("LoyaltyProgramId");

                    b.HasIndex("UserId");

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
                    b.HasOne("ISA.Core.Domain.Entities.Company.Company", null)
                        .WithMany("Appointments")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.Company.Company", b =>
                {
                    b.HasOne("ISA.Core.Domain.Entities.User.Address", "Address")
                        .WithOne()
                        .HasForeignKey("ISA.Core.Domain.Entities.Company.Company", "AddresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("ISA.Core.Domain.Entities.Company.Equipment", b =>
                {
                    b.HasOne("ISA.Core.Domain.Entities.Company.Company", null)
                        .WithMany("Equipment")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
#pragma warning restore 612, 618
        }
    }
}

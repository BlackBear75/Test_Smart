﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Test_Smart.Configuration;

#nullable disable

namespace Test_Smart.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241213084145_InitBase")]
    partial class InitBase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Test_Smart.Entity.EquipmentType.EquipmentType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("AreaPerUnit")
                        .HasColumnType("float");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DeletionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("EquipmentTypes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("50a9bf64-de61-4953-b100-4e8942cfc0d5"),
                            AreaPerUnit = 50.0,
                            Code = "EQ001",
                            CreationDate = new DateTime(2024, 12, 13, 8, 41, 44, 270, DateTimeKind.Utc),
                            Deleted = false,
                            Name = "Machine A"
                        },
                        new
                        {
                            Id = new Guid("3c6cd489-4d2e-44bf-887e-c40595806b42"),
                            AreaPerUnit = 70.0,
                            Code = "EQ002",
                            CreationDate = new DateTime(2024, 12, 13, 8, 41, 44, 270, DateTimeKind.Utc),
                            Deleted = false,
                            Name = "Machine B"
                        },
                        new
                        {
                            Id = new Guid("426e971d-5686-4893-9ff0-57338b393744"),
                            AreaPerUnit = 30.0,
                            Code = "EQ003",
                            CreationDate = new DateTime(2024, 12, 13, 8, 41, 44, 270, DateTimeKind.Utc),
                            Deleted = false,
                            Name = "Machine C"
                        },
                        new
                        {
                            Id = new Guid("3acd1f1f-eff9-4d5d-9413-7a42f79b5505"),
                            AreaPerUnit = 90.0,
                            Code = "EQ004",
                            CreationDate = new DateTime(2024, 12, 13, 8, 41, 44, 270, DateTimeKind.Utc),
                            Deleted = false,
                            Name = "Machine D"
                        });
                });

            modelBuilder.Entity("Test_Smart.Entity.PlacementContract.PlacementContract", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DeletionDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("EquipmentTypeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductionFacilityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EquipmentTypeId");

                    b.HasIndex("ProductionFacilityId");

                    b.ToTable("PlacementContracts");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e1258336-4138-437f-9a90-c85875b6f575"),
                            CreationDate = new DateTime(2024, 12, 13, 8, 41, 44, 270, DateTimeKind.Utc),
                            Deleted = false,
                            EquipmentTypeId = new Guid("50a9bf64-de61-4953-b100-4e8942cfc0d5"),
                            ProductionFacilityId = new Guid("d1c64f7c-379f-4340-a4aa-e48a27155849"),
                            Quantity = 10
                        },
                        new
                        {
                            Id = new Guid("f8ce946f-a080-413d-b3e0-409e7ccaa7b3"),
                            CreationDate = new DateTime(2024, 12, 13, 8, 41, 44, 270, DateTimeKind.Utc),
                            Deleted = false,
                            EquipmentTypeId = new Guid("3c6cd489-4d2e-44bf-887e-c40595806b42"),
                            ProductionFacilityId = new Guid("e2b25bd1-7902-4d1d-8a80-dbb97a6f4ef2"),
                            Quantity = 5
                        },
                        new
                        {
                            Id = new Guid("b7e98a07-1f76-4057-9974-3f4a5fa4c8ad"),
                            CreationDate = new DateTime(2024, 12, 13, 8, 41, 44, 270, DateTimeKind.Utc),
                            Deleted = false,
                            EquipmentTypeId = new Guid("426e971d-5686-4893-9ff0-57338b393744"),
                            ProductionFacilityId = new Guid("1eece660-1422-4254-8d8b-90dad255b037"),
                            Quantity = 15
                        },
                        new
                        {
                            Id = new Guid("862afda1-b4c8-4871-8729-5db3cd231f5d"),
                            CreationDate = new DateTime(2024, 12, 13, 8, 41, 44, 270, DateTimeKind.Utc),
                            Deleted = false,
                            EquipmentTypeId = new Guid("3acd1f1f-eff9-4d5d-9413-7a42f79b5505"),
                            ProductionFacilityId = new Guid("d1c64f7c-379f-4340-a4aa-e48a27155849"),
                            Quantity = 8
                        },
                        new
                        {
                            Id = new Guid("45b24272-4183-46a4-9d07-655e44da32a2"),
                            CreationDate = new DateTime(2024, 12, 13, 8, 41, 44, 270, DateTimeKind.Utc),
                            Deleted = false,
                            EquipmentTypeId = new Guid("426e971d-5686-4893-9ff0-57338b393744"),
                            ProductionFacilityId = new Guid("e2b25bd1-7902-4d1d-8a80-dbb97a6f4ef2"),
                            Quantity = 20
                        });
                });

            modelBuilder.Entity("Test_Smart.Entity.ProductionFacility.ProductionFacility", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("DeletionDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<double>("StandardArea")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("ProductionFacilities");

                    b.HasData(
                        new
                        {
                            Id = new Guid("d1c64f7c-379f-4340-a4aa-e48a27155849"),
                            Code = "FAC001",
                            CreationDate = new DateTime(2024, 12, 13, 8, 41, 44, 270, DateTimeKind.Utc),
                            Deleted = false,
                            Name = "Factory A",
                            StandardArea = 1000.0
                        },
                        new
                        {
                            Id = new Guid("e2b25bd1-7902-4d1d-8a80-dbb97a6f4ef2"),
                            Code = "FAC002",
                            CreationDate = new DateTime(2024, 12, 13, 8, 41, 44, 270, DateTimeKind.Utc),
                            Deleted = false,
                            Name = "Factory B",
                            StandardArea = 800.0
                        },
                        new
                        {
                            Id = new Guid("1eece660-1422-4254-8d8b-90dad255b037"),
                            Code = "FAC003",
                            CreationDate = new DateTime(2024, 12, 13, 8, 41, 44, 270, DateTimeKind.Utc),
                            Deleted = false,
                            Name = "Factory C",
                            StandardArea = 1200.0
                        });
                });

            modelBuilder.Entity("Test_Smart.Entity.PlacementContract.PlacementContract", b =>
                {
                    b.HasOne("Test_Smart.Entity.EquipmentType.EquipmentType", "EquipmentType")
                        .WithMany()
                        .HasForeignKey("EquipmentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Test_Smart.Entity.ProductionFacility.ProductionFacility", "ProductionFacility")
                        .WithMany()
                        .HasForeignKey("ProductionFacilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EquipmentType");

                    b.Navigation("ProductionFacility");
                });
#pragma warning restore 612, 618
        }
    }
}
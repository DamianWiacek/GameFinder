﻿// <auto-generated />
using System;
using GameFinder.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GameFinder.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230404181410_City_Changed_To_Address")]
    partial class City_Changed_To_Address
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("GameFinder.Domain.Entities.Address", b =>
                {
                    b.Property<int>("Address_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Address_Id"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Postal_Code")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Address_Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("GameFinder.Domain.Entities.Court", b =>
                {
                    b.Property<int>("Court_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Court_Id"), 1L, 1);

                    b.Property<int>("Address_Id")
                        .HasColumnType("int");

                    b.HasKey("Court_Id");

                    b.ToTable("Court");
                });

            modelBuilder.Entity("GameFinder.Domain.Entities.Game", b =>
                {
                    b.Property<int>("Game_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Game_Id"), 1L, 1);

                    b.Property<int>("Court_Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Precicted_End")
                        .HasColumnType("datetime2");

                    b.Property<int>("Sport_Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.HasKey("Game_Id");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("GameFinder.Domain.Entities.Game_Details", b =>
                {
                    b.Property<int>("Game_Details_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Game_Details_Id"), 1L, 1);

                    b.Property<int>("Game_Id")
                        .HasColumnType("int");

                    b.Property<int>("User_Id")
                        .HasColumnType("int");

                    b.HasKey("Game_Details_Id");

                    b.ToTable("Game_Details");
                });

            modelBuilder.Entity("GameFinder.Domain.Entities.Role", b =>
                {
                    b.Property<int>("Role_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Role_Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Role_Id");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            Role_Id = 1,
                            Name = "Player"
                        },
                        new
                        {
                            Role_Id = 2,
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("GameFinder.Domain.Entities.Sport", b =>
                {
                    b.Property<int>("Sport_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Sport_Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Sport_Id");

                    b.ToTable("Sport");

                    b.HasData(
                        new
                        {
                            Sport_Id = 1,
                            Name = "Soccer"
                        },
                        new
                        {
                            Sport_Id = 2,
                            Name = "Basketball"
                        },
                        new
                        {
                            Sport_Id = 3,
                            Name = "Volleyball"
                        },
                        new
                        {
                            Sport_Id = 4,
                            Name = "Tennis"
                        });
                });

            modelBuilder.Entity("GameFinder.Domain.Entities.User", b =>
                {
                    b.Property<int>("User_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("User_Id"), 1L, 1);

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password_Hash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role_Id")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("User_Id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}

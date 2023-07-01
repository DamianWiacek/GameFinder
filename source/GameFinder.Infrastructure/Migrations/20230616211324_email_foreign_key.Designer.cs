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
    [Migration("20230616211324_email_foreign_key")]
    partial class email_foreign_key
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
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AddressId"), 1L, 1);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AddressId");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("GameFinder.Domain.Entities.Court", b =>
                {
                    b.Property<int>("CourtId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourtId"), 1L, 1);

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<string>("CourtType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourtId");

                    b.HasIndex("AddressId");

                    b.ToTable("Court");
                });

            modelBuilder.Entity("GameFinder.Domain.Entities.EMail", b =>
                {
                    b.Property<int>("EmailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EmailId"), 1L, 1);

                    b.Property<bool>("IsSent")
                        .HasColumnType("bit");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserEmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("EmailId");

                    b.HasIndex("UserEmailAddress");

                    b.ToTable("EMail");
                });

            modelBuilder.Entity("GameFinder.Domain.Entities.Game", b =>
                {
                    b.Property<int>("GameId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GameId"), 1L, 1);

                    b.Property<int>("CourtId")
                        .HasColumnType("int");

                    b.Property<DateTime>("PredictedEnd")
                        .HasColumnType("datetime2");

                    b.Property<int>("SportId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.HasKey("GameId");

                    b.HasIndex("CourtId");

                    b.HasIndex("SportId");

                    b.ToTable("Game");
                });

            modelBuilder.Entity("GameFinder.Domain.Entities.GameDetails", b =>
                {
                    b.Property<int>("GameDetailsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GameDetailsId"), 1L, 1);

                    b.Property<int>("GameId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("GameDetailsId");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("Game_Details");
                });

            modelBuilder.Entity("GameFinder.Domain.Entities.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Role");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            Name = "Player"
                        },
                        new
                        {
                            RoleId = 2,
                            Name = "Administrator"
                        });
                });

            modelBuilder.Entity("GameFinder.Domain.Entities.Sport", b =>
                {
                    b.Property<int>("SportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SportId"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SportId");

                    b.ToTable("Sport");

                    b.HasData(
                        new
                        {
                            SportId = 1,
                            Name = "Soccer"
                        },
                        new
                        {
                            SportId = 2,
                            Name = "Basketball"
                        },
                        new
                        {
                            SportId = 3,
                            Name = "Volleyball"
                        },
                        new
                        {
                            SportId = 4,
                            Name = "Tennis"
                        });
                });

            modelBuilder.Entity("GameFinder.Domain.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Pesel")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("GameFinder.Domain.Entities.Court", b =>
                {
                    b.HasOne("GameFinder.Domain.Entities.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Address");
                });

            modelBuilder.Entity("GameFinder.Domain.Entities.EMail", b =>
                {
                    b.HasOne("GameFinder.Domain.Entities.User", "User")
                        .WithMany("Emails")
                        .HasForeignKey("UserEmailAddress")
                        .HasPrincipalKey("EmailAddress")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("GameFinder.Domain.Entities.Game", b =>
                {
                    b.HasOne("GameFinder.Domain.Entities.Court", "Court")
                        .WithMany("Games")
                        .HasForeignKey("CourtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameFinder.Domain.Entities.Sport", "Sport")
                        .WithMany()
                        .HasForeignKey("SportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Court");

                    b.Navigation("Sport");
                });

            modelBuilder.Entity("GameFinder.Domain.Entities.GameDetails", b =>
                {
                    b.HasOne("GameFinder.Domain.Entities.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GameFinder.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("GameFinder.Domain.Entities.User", b =>
                {
                    b.HasOne("GameFinder.Domain.Entities.Role", "RoleRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoleRole");
                });

            modelBuilder.Entity("GameFinder.Domain.Entities.Court", b =>
                {
                    b.Navigation("Games");
                });

            modelBuilder.Entity("GameFinder.Domain.Entities.User", b =>
                {
                    b.Navigation("Emails");
                });
#pragma warning restore 612, 618
        }
    }
}

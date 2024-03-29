﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Stock_Back.DAL.Context;

#nullable disable

namespace Stock_Back.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240111145230_vigency-attribute")]
    partial class vigencyattribute
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Stock_Back.DAL.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Phone")
                        .HasColumnType("integer");

                    b.Property<string>("Rut")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Vigency")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("client");
                });

            modelBuilder.Entity("Stock_Back.DAL.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Created")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Phone")
                        .HasColumnType("integer");

                    b.Property<bool>("SuperAdmin")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Updated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("Vigency")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Created = new DateTime(2024, 1, 11, 14, 52, 30, 16, DateTimeKind.Utc).AddTicks(9464),
                            Email = "admin@admin.cl",
                            Name = "",
                            Password = "q8StP6cPfMLCCr4O8iKMcKU6K2U2E6j3WJsjO4JjPLUNeBk2",
                            Phone = 0,
                            SuperAdmin = true,
                            Updated = new DateTime(2024, 1, 11, 14, 52, 30, 16, DateTimeKind.Utc).AddTicks(9465),
                            Vigency = false
                        });
                });
#pragma warning restore 612, 618
        }
    }
}

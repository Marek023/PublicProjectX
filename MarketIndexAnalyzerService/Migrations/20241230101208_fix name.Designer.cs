﻿// <auto-generated />
using System;
using MarketIndexAnalyzer.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MarketIndexAnalyzer.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241230101208_fix name")]
    partial class fixname
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("MarketIndexAnalyzer.Data.Entities.IndexData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("Close")
                        .HasColumnType("double precision");

                    b.Property<double>("High")
                        .HasColumnType("double precision");

                    b.Property<int>("IndexTypeId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<double>("Low")
                        .HasColumnType("double precision");

                    b.Property<double>("Open")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("Time")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("IndexTypeId");

                    b.ToTable("IndexData");
                });

            modelBuilder.Entity("MarketIndexAnalyzer.Data.Entities.IndexType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Currency")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("character varying(200)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Symbol")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("IndexType");
                });

            modelBuilder.Entity("MarketIndexAnalyzer.Data.Entities.IndexData", b =>
                {
                    b.HasOne("MarketIndexAnalyzer.Data.Entities.IndexType", "IndexType")
                        .WithMany("IndexData")
                        .HasForeignKey("IndexTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IndexType");
                });

            modelBuilder.Entity("MarketIndexAnalyzer.Data.Entities.IndexType", b =>
                {
                    b.Navigation("IndexData");
                });
#pragma warning restore 612, 618
        }
    }
}

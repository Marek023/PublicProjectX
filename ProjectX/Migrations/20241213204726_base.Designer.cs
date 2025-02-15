﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProjectX.Data.Contexts;

#nullable disable

namespace ProjectX.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241213204726_base")]
    partial class @base
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ProjectX.Data.Entities.AssetCategories", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("AssetCategories");
                });

            modelBuilder.Entity("ProjectX.Data.Entities.AssetHistorical", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("AddedSecurity")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<int>("AssetId")
                        .HasColumnType("integer");

                    b.Property<string>("Date")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("DateAdded")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime>("DateSave")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Reason")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("RemovedSecurity")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("RemovedTicker")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Symbol")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("AssetId")
                        .IsUnique();

                    b.ToTable("AssetHistorical");
                });

            modelBuilder.Entity("ProjectX.Data.Entities.AssetHistoricalData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal>("AdjClose")
                        .HasColumnType("numeric");

                    b.Property<int>("AssetsId")
                        .HasColumnType("integer");

                    b.Property<decimal>("Change")
                        .HasColumnType("numeric");

                    b.Property<decimal>("ChangeOverTime")
                        .HasColumnType("numeric");

                    b.Property<decimal>("ChangePercent")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Close")
                        .HasColumnType("numeric");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime?>("DateSave")
                        .HasColumnType("timestamp with time zone");

                    b.Property<decimal>("High")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Low")
                        .HasColumnType("numeric");

                    b.Property<decimal>("Open")
                        .HasColumnType("numeric");

                    b.Property<long>("UnadjustedVolume")
                        .HasColumnType("bigint");

                    b.Property<long>("Volume")
                        .HasColumnType("bigint");

                    b.Property<decimal>("Vwap")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("AssetsId");

                    b.ToTable("AssetHistoricalData");
                });

            modelBuilder.Entity("ProjectX.Data.Entities.Assets", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AssetCategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Cik")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("DateFirstAdded")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("DateSave")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Founded")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("HeadQuarter")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Sector")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("SubSector")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Symbol")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("AssetCategoryId");

                    b.ToTable("Assets");
                });

            modelBuilder.Entity("ProjectX.Data.Entities.ExcludedAssets", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AssetCategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Cik")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("DateFirstAdded")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("DateSave")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Founded")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("HeadQuarter")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("NotificationSent")
                        .HasColumnType("boolean");

                    b.Property<string>("Sector")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("SubSector")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Symbol")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("AssetCategoryId");

                    b.ToTable("ExcludedAssets");
                });

            modelBuilder.Entity("ProjectX.Data.Entities.NewAssets", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("AssetCategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("Cik")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("DateFirstAdded")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTime?>("DateSave")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Founded")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("HeadQuarter")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("NotificationSent")
                        .HasColumnType("boolean");

                    b.Property<string>("Sector")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("SubSector")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Symbol")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("AssetCategoryId");

                    b.ToTable("NewAssets");
                });

            modelBuilder.Entity("ProjectX.Data.Entities.AssetHistorical", b =>
                {
                    b.HasOne("ProjectX.Data.Entities.Assets", "Asset")
                        .WithOne("AssetHistorical")
                        .HasForeignKey("ProjectX.Data.Entities.AssetHistorical", "AssetId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Asset");
                });

            modelBuilder.Entity("ProjectX.Data.Entities.AssetHistoricalData", b =>
                {
                    b.HasOne("ProjectX.Data.Entities.Assets", "Assets")
                        .WithMany("AssetHistoricalDatas")
                        .HasForeignKey("AssetsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assets");
                });

            modelBuilder.Entity("ProjectX.Data.Entities.Assets", b =>
                {
                    b.HasOne("ProjectX.Data.Entities.AssetCategories", "AssetCategory")
                        .WithMany("Assets")
                        .HasForeignKey("AssetCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssetCategory");
                });

            modelBuilder.Entity("ProjectX.Data.Entities.ExcludedAssets", b =>
                {
                    b.HasOne("ProjectX.Data.Entities.AssetCategories", "AssetCategory")
                        .WithMany("ExcludedAssets")
                        .HasForeignKey("AssetCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssetCategory");
                });

            modelBuilder.Entity("ProjectX.Data.Entities.NewAssets", b =>
                {
                    b.HasOne("ProjectX.Data.Entities.AssetCategories", "AssetCategory")
                        .WithMany("NewAssets")
                        .HasForeignKey("AssetCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssetCategory");
                });

            modelBuilder.Entity("ProjectX.Data.Entities.AssetCategories", b =>
                {
                    b.Navigation("Assets");

                    b.Navigation("ExcludedAssets");

                    b.Navigation("NewAssets");
                });

            modelBuilder.Entity("ProjectX.Data.Entities.Assets", b =>
                {
                    b.Navigation("AssetHistorical");

                    b.Navigation("AssetHistoricalDatas");
                });
#pragma warning restore 612, 618
        }
    }
}

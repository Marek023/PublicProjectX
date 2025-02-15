using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjectX.Migrations
{
    /// <inheritdoc />
    public partial class @base : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AssetCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Symbol = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Sector = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SubSector = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    HeadQuarter = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DateFirstAdded = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Cik = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Founded = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DateSave = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AssetCategoryId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assets_AssetCategories_AssetCategoryId",
                        column: x => x.AssetCategoryId,
                        principalTable: "AssetCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExcludedAssets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Symbol = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Sector = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SubSector = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    HeadQuarter = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DateFirstAdded = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Cik = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Founded = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DateSave = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    NotificationSent = table.Column<bool>(type: "boolean", nullable: false),
                    AssetCategoryId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcludedAssets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExcludedAssets_AssetCategories_AssetCategoryId",
                        column: x => x.AssetCategoryId,
                        principalTable: "AssetCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewAssets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Symbol = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Sector = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    SubSector = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    HeadQuarter = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DateFirstAdded = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Cik = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Founded = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DateSave = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    NotificationSent = table.Column<bool>(type: "boolean", nullable: false),
                    AssetCategoryId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewAssets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NewAssets_AssetCategories_AssetCategoryId",
                        column: x => x.AssetCategoryId,
                        principalTable: "AssetCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetHistorical",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DateAdded = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AddedSecurity = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    RemovedTicker = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    RemovedSecurity = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Date = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Symbol = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Reason = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    DateSave = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    AssetId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetHistorical", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetHistorical_Assets_AssetId",
                        column: x => x.AssetId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetHistoricalData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<string>(type: "text", nullable: false),
                    Open = table.Column<decimal>(type: "numeric", nullable: false),
                    Low = table.Column<decimal>(type: "numeric", nullable: false),
                    High = table.Column<decimal>(type: "numeric", nullable: false),
                    Close = table.Column<decimal>(type: "numeric", nullable: false),
                    AdjClose = table.Column<decimal>(type: "numeric", nullable: false),
                    Volume = table.Column<long>(type: "bigint", nullable: false),
                    UnadjustedVolume = table.Column<long>(type: "bigint", nullable: false),
                    Change = table.Column<decimal>(type: "numeric", nullable: false),
                    ChangePercent = table.Column<decimal>(type: "numeric", nullable: false),
                    Vwap = table.Column<decimal>(type: "numeric", nullable: false),
                    ChangeOverTime = table.Column<decimal>(type: "numeric", nullable: false),
                    DateSave = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AssetsId = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetHistoricalData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetHistoricalData_Assets_AssetsId",
                        column: x => x.AssetsId,
                        principalTable: "Assets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AssetHistorical_AssetId",
                table: "AssetHistorical",
                column: "AssetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AssetHistoricalData_AssetsId",
                table: "AssetHistoricalData",
                column: "AssetsId");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_AssetCategoryId",
                table: "Assets",
                column: "AssetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ExcludedAssets_AssetCategoryId",
                table: "ExcludedAssets",
                column: "AssetCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_NewAssets_AssetCategoryId",
                table: "NewAssets",
                column: "AssetCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AssetHistorical");

            migrationBuilder.DropTable(
                name: "AssetHistoricalData");

            migrationBuilder.DropTable(
                name: "ExcludedAssets");

            migrationBuilder.DropTable(
                name: "NewAssets");

            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "AssetCategories");
        }
    }
}

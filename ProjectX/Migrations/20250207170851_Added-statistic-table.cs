using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ProjectX.Migrations
{
    /// <inheritdoc />
    public partial class Addedstatistictable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TotalYearStatistic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    Month = table.Column<int>(type: "integer", nullable: false),
                    TotalDeposit = table.Column<int>(type: "integer", nullable: false),
                    TotalProfitForYear = table.Column<decimal>(type: "numeric", nullable: false),
                    ProfitInPercentForYear = table.Column<decimal>(type: "numeric", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TotalYearStatistic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "YearStatistic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalYearStatisticId = table.Column<int>(type: "integer", nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    MonthNumber = table.Column<int>(type: "integer", nullable: false),
                    MonthName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    TotalDeposit = table.Column<int>(type: "integer", nullable: false),
                    Profit = table.Column<decimal>(type: "numeric", nullable: false),
                    Dividend = table.Column<decimal>(type: "numeric", nullable: false),
                    ProfitInPercent = table.Column<decimal>(type: "numeric", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearStatistic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YearStatistic_TotalYearStatistic_TotalYearStatisticId",
                        column: x => x.TotalYearStatisticId,
                        principalTable: "TotalYearStatistic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_YearStatistic_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MonthStatistic",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    YearStatisticId = table.Column<int>(type: "integer", nullable: false),
                    Symbol = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Volume = table.Column<int>(type: "integer", nullable: false),
                    OpenTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    OpenPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    CloseTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Profit = table.Column<decimal>(type: "numeric", nullable: false),
                    ProfitInPercent = table.Column<decimal>(type: "numeric", nullable: false),
                    TotalProfitInPercent = table.Column<decimal>(type: "numeric", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonthStatistic", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MonthStatistic_YearStatistic_YearStatisticId",
                        column: x => x.YearStatisticId,
                        principalTable: "YearStatistic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MonthStatistic_YearStatisticId",
                table: "MonthStatistic",
                column: "YearStatisticId");

            migrationBuilder.CreateIndex(
                name: "IX_YearStatistic_TotalYearStatisticId",
                table: "YearStatistic",
                column: "TotalYearStatisticId");

            migrationBuilder.CreateIndex(
                name: "IX_YearStatistic_UserId",
                table: "YearStatistic",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MonthStatistic");

            migrationBuilder.DropTable(
                name: "YearStatistic");

            migrationBuilder.DropTable(
                name: "TotalYearStatistic");
        }
    }
}

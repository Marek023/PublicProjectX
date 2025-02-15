using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketIndexAnalyzer.Migrations
{
    /// <inheritdoc />
    public partial class fixname : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndexData_IndexTypes_IndexTypeId",
                table: "IndexData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IndexTypes",
                table: "IndexTypes");

            migrationBuilder.RenameTable(
                name: "IndexTypes",
                newName: "IndexType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndexType",
                table: "IndexType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IndexData_IndexType_IndexTypeId",
                table: "IndexData",
                column: "IndexTypeId",
                principalTable: "IndexType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IndexData_IndexType_IndexTypeId",
                table: "IndexData");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IndexType",
                table: "IndexType");

            migrationBuilder.RenameTable(
                name: "IndexType",
                newName: "IndexTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IndexTypes",
                table: "IndexTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IndexData_IndexTypes_IndexTypeId",
                table: "IndexData",
                column: "IndexTypeId",
                principalTable: "IndexTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

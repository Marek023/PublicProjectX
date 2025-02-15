using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectX.Migrations
{
    /// <inheritdoc />
    public partial class NotificationCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NotificationSent",
                table: "NewAssets",
                newName: "NotificationCreated");

            migrationBuilder.RenameColumn(
                name: "NotificationSent",
                table: "ExcludedAssets",
                newName: "NotificationCreated");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NotificationCreated",
                table: "NewAssets",
                newName: "NotificationSent");

            migrationBuilder.RenameColumn(
                name: "NotificationCreated",
                table: "ExcludedAssets",
                newName: "NotificationSent");
        }
    }
}

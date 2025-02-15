using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectX.Migrations
{
    /// <inheritdoc />
    public partial class reload : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSettingAssetNotificationQueue_AssetNotificationQueue_As~",
                table: "UserSettingAssetNotificationQueue");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSettingAssetNotificationQueue_UserSetting_UserSettingId",
                table: "UserSettingAssetNotificationQueue");

            migrationBuilder.AlterColumn<int>(
                name: "UserSettingId",
                table: "UserSettingAssetNotificationQueue",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AssetNotificationQueueId",
                table: "UserSettingAssetNotificationQueue",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSettingAssetNotificationQueue_AssetNotificationQueue_As~",
                table: "UserSettingAssetNotificationQueue",
                column: "AssetNotificationQueueId",
                principalTable: "AssetNotificationQueue",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSettingAssetNotificationQueue_UserSetting_UserSettingId",
                table: "UserSettingAssetNotificationQueue",
                column: "UserSettingId",
                principalTable: "UserSetting",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSettingAssetNotificationQueue_AssetNotificationQueue_As~",
                table: "UserSettingAssetNotificationQueue");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSettingAssetNotificationQueue_UserSetting_UserSettingId",
                table: "UserSettingAssetNotificationQueue");

            migrationBuilder.AlterColumn<int>(
                name: "UserSettingId",
                table: "UserSettingAssetNotificationQueue",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "AssetNotificationQueueId",
                table: "UserSettingAssetNotificationQueue",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSettingAssetNotificationQueue_AssetNotificationQueue_As~",
                table: "UserSettingAssetNotificationQueue",
                column: "AssetNotificationQueueId",
                principalTable: "AssetNotificationQueue",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSettingAssetNotificationQueue_UserSetting_UserSettingId",
                table: "UserSettingAssetNotificationQueue",
                column: "UserSettingId",
                principalTable: "UserSetting",
                principalColumn: "Id");
        }
    }
}

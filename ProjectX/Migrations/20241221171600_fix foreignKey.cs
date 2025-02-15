using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectX.Migrations
{
    /// <inheritdoc />
    public partial class fixforeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSettingAssetNotificationQueue_AssetNotificationQueue_Us~",
                table: "UserSettingAssetNotificationQueue");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSettingAssetNotificationQueue_UserSetting_AssetNotifica~",
                table: "UserSettingAssetNotificationQueue");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSettingAssetNotificationQueue_AssetNotificationQueue_As~",
                table: "UserSettingAssetNotificationQueue");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSettingAssetNotificationQueue_UserSetting_UserSettingId",
                table: "UserSettingAssetNotificationQueue");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSettingAssetNotificationQueue_AssetNotificationQueue_Us~",
                table: "UserSettingAssetNotificationQueue",
                column: "UserSettingId",
                principalTable: "AssetNotificationQueue",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSettingAssetNotificationQueue_UserSetting_AssetNotifica~",
                table: "UserSettingAssetNotificationQueue",
                column: "AssetNotificationQueueId",
                principalTable: "UserSetting",
                principalColumn: "Id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameFinder.Infrastructure.Migrations
{
    public partial class relationsFixedIHopeSoXd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_User_UserId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_UserId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notification");

            migrationBuilder.CreateIndex(
                name: "IX_UserInbox_UserId",
                table: "UserInbox",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserInbox_User_UserId",
                table: "UserInbox",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInbox_User_UserId",
                table: "UserInbox");

            migrationBuilder.DropIndex(
                name: "IX_UserInbox_UserId",
                table: "UserInbox");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Notification",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_User_UserId",
                table: "Notification",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId");
        }
    }
}

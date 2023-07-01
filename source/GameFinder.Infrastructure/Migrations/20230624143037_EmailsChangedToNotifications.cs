using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameFinder.Infrastructure.Migrations
{
    public partial class EmailsChangedToNotifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EMail");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_User_EmailAddress",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateTable(
                name: "UserInbox",
                columns: table => new
                {
                    UserInboxId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInbox", x => x.UserInboxId);
                });

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsSent = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserInboxId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notification_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Notification_UserInbox_UserInboxId",
                        column: x => x.UserInboxId,
                        principalTable: "UserInbox",
                        principalColumn: "UserInboxId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserInboxId",
                table: "Notification",
                column: "UserInboxId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "UserInbox");

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "User",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_User_EmailAddress",
                table: "User",
                column: "EmailAddress");

            migrationBuilder.CreateTable(
                name: "EMail",
                columns: table => new
                {
                    EmailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserEmailAddress = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsSent = table.Column<bool>(type: "bit", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMail", x => x.EmailId);
                    table.ForeignKey(
                        name: "FK_EMail_User_UserEmailAddress",
                        column: x => x.UserEmailAddress,
                        principalTable: "User",
                        principalColumn: "EmailAddress",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EMail_UserEmailAddress",
                table: "EMail",
                column: "UserEmailAddress");
        }
    }
}

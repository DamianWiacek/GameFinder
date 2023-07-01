using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameFinder.Infrastructure.Migrations
{
    public partial class email_foreign_key : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "User",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmailAddress",
                table: "EMail",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_User_EmailAddress",
                table: "User",
                column: "EmailAddress");

            migrationBuilder.CreateIndex(
                name: "IX_EMail_UserEmailAddress",
                table: "EMail",
                column: "UserEmailAddress");

            migrationBuilder.AddForeignKey(
                name: "FK_EMail_User_UserEmailAddress",
                table: "EMail",
                column: "UserEmailAddress",
                principalTable: "User",
                principalColumn: "EmailAddress",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EMail_User_UserEmailAddress",
                table: "EMail");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_User_EmailAddress",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_EMail_UserEmailAddress",
                table: "EMail");

            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmailAddress",
                table: "EMail",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}

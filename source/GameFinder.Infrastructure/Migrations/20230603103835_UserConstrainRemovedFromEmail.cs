using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameFinder.Infrastructure.Migrations
{
    public partial class UserConstrainRemovedFromEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EMail_User_UserId",
                table: "EMail");

            migrationBuilder.DropIndex(
                name: "IX_EMail_UserId",
                table: "EMail");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EMail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "EMail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_EMail_UserId",
                table: "EMail",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_EMail_User_UserId",
                table: "EMail",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

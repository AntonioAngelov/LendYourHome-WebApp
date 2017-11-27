using Microsoft.EntityFrameworkCore.Migrations;

namespace LendYourHome.Data.Migrations
{
    public partial class ReviewsUersRelationChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_GuestId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "GuestId",
                table: "Reviews",
                newName: "EvaluatingGuestId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_GuestId",
                table: "Reviews",
                newName: "IX_Reviews_EvaluatingGuestId");

            migrationBuilder.AddColumn<string>(
                name: "EvaluatedGuestId",
                table: "Reviews",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_EvaluatedGuestId",
                table: "Reviews",
                column: "EvaluatedGuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_EvaluatedGuestId",
                table: "Reviews",
                column: "EvaluatedGuestId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_EvaluatingGuestId",
                table: "Reviews",
                column: "EvaluatingGuestId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_EvaluatedGuestId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_EvaluatingGuestId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_EvaluatedGuestId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "EvaluatedGuestId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "EvaluatingGuestId",
                table: "Reviews",
                newName: "GuestId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_EvaluatingGuestId",
                table: "Reviews",
                newName: "IX_Reviews_GuestId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_GuestId",
                table: "Reviews",
                column: "GuestId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

namespace LendYourHome.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class AddedReviewAbstractTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Review_AspNetUsers_GuestId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_AspNetUsers_HostId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Homes_HomeId",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "Reviews");

            migrationBuilder.RenameIndex(
                name: "IX_Review_HomeId",
                table: "Reviews",
                newName: "IX_Reviews_HomeId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_HostId",
                table: "Reviews",
                newName: "IX_Reviews_HostId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_GuestId",
                table: "Reviews",
                newName: "IX_Reviews_GuestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_GuestId",
                table: "Reviews",
                column: "GuestId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_HostId",
                table: "Reviews",
                column: "HostId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Homes_HomeId",
                table: "Reviews",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_GuestId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_HostId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Homes_HomeId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Review");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_HomeId",
                table: "Review",
                newName: "IX_Review_HomeId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_HostId",
                table: "Review",
                newName: "IX_Review_HostId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_GuestId",
                table: "Review",
                newName: "IX_Review_GuestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_AspNetUsers_GuestId",
                table: "Review",
                column: "GuestId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_AspNetUsers_HostId",
                table: "Review",
                column: "HostId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Homes_HomeId",
                table: "Review",
                column: "HomeId",
                principalTable: "Homes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace LendYourHome.Data.Migrations
{
    public partial class AddedSomeValidation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AdditionalThoughts",
                table: "Reviews",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Pictures",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "AdditionalThoughts",
                table: "Reviews",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Pictures",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}

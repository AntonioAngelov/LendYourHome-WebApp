using Microsoft.EntityFrameworkCore.Migrations;

namespace LendYourHome.Data.Migrations
{
    public partial class ReviewEvaluationIsInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Evaluation",
                table: "Reviews",
                nullable: false,
                oldClrType: typeof(byte));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Evaluation",
                table: "Reviews",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}

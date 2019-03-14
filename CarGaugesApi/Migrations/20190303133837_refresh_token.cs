using Microsoft.EntityFrameworkCore.Migrations;

namespace CarGaugesApi.Migrations
{
    public partial class refresh_token : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RefrestToken",
                table: "User",
                nullable: true,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefrestToken",
                table: "User");
        }
    }
}

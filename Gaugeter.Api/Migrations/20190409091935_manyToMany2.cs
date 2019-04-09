using Microsoft.EntityFrameworkCore.Migrations;

namespace Gaugeter.Api.Migrations
{
    public partial class manyToMany2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Device_User_UserId",
                table: "Device");

            migrationBuilder.DropIndex(
                name: "IX_Device_UserId",
                table: "Device");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Device");

            migrationBuilder.CreateTable(
                name: "UserDevice",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    BluetoothAddress = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDevice", x => new { x.UserId, x.BluetoothAddress });
                    table.ForeignKey(
                        name: "FK_UserDevice_Device_BluetoothAddress",
                        column: x => x.BluetoothAddress,
                        principalTable: "Device",
                        principalColumn: "BluetoothAddress",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserDevice_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDevice_BluetoothAddress",
                table: "UserDevice",
                column: "BluetoothAddress");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDevice");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Device",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Device_UserId",
                table: "Device",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Device_User_UserId",
                table: "Device",
                column: "UserId",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

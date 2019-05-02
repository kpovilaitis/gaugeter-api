using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gaugeter.Api.Migrations
{
    public partial class addedJobs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Duration",
                table: "Job",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "DeviceAddress",
                table: "Job",
                newName: "UserId");

            migrationBuilder.AddColumn<long>(
                name: "DateCreated",
                table: "Job",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "DateUpdated",
                table: "Job",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "DeviceBluetoothAddress",
                table: "Job",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TelemData",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    OilTemperature = table.Column<float>(nullable: false),
                    OilPressure = table.Column<float>(nullable: false),
                    WaterTemperature = table.Column<float>(nullable: false),
                    Charge = table.Column<float>(nullable: false),
                    JobId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TelemData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TelemData_Job_JobId",
                        column: x => x.JobId,
                        principalTable: "Job",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Job_DeviceBluetoothAddress",
                table: "Job",
                column: "DeviceBluetoothAddress");

            migrationBuilder.CreateIndex(
                name: "IX_TelemData_JobId",
                table: "TelemData",
                column: "JobId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Device_DeviceBluetoothAddress",
                table: "Job",
                column: "DeviceBluetoothAddress",
                principalTable: "Device",
                principalColumn: "BluetoothAddress",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Device_DeviceBluetoothAddress",
                table: "Job");

            migrationBuilder.DropTable(
                name: "TelemData");

            migrationBuilder.DropIndex(
                name: "IX_Job_DeviceBluetoothAddress",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "DateUpdated",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "DeviceBluetoothAddress",
                table: "Job");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Job",
                newName: "DeviceAddress");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Job",
                newName: "Duration");
        }
    }
}

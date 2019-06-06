using Microsoft.EntityFrameworkCore.Migrations;

namespace Gaugeter.Api.Migrations
{
    public partial class updateTelemDataWithTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TelemData_Job_JobId",
                table: "TelemData");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "TelemData",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DateCreated",
                table: "TelemData",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_TelemData_Job_JobId",
                table: "TelemData",
                column: "JobId",
                principalTable: "Job",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TelemData_Job_JobId",
                table: "TelemData");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "TelemData");

            migrationBuilder.AlterColumn<int>(
                name: "JobId",
                table: "TelemData",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_TelemData_Job_JobId",
                table: "TelemData",
                column: "JobId",
                principalTable: "Job",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

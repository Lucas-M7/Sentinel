using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sentinel.Api.Migrations
{
    /// <inheritdoc />
    public partial class DomainHardening : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CheckLogs_MonitorId",
                table: "CheckLogs");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Monitors",
                type: "character varying(2048)",
                maxLength: 2048,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Monitors",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Response",
                table: "CheckLogs",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Monitors_IsActive_NextCheckAt",
                table: "Monitors",
                columns: new[] { "IsActive", "NextCheckAt" });

            migrationBuilder.CreateIndex(
                name: "IX_CheckLogs_MonitorId_CreatedAt",
                table: "CheckLogs",
                columns: new[] { "MonitorId", "CreatedAt" },
                descending: new[] { false, true });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Monitors_IsActive_NextCheckAt",
                table: "Monitors");

            migrationBuilder.DropIndex(
                name: "IX_CheckLogs_MonitorId_CreatedAt",
                table: "CheckLogs");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Monitors",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(2048)",
                oldMaxLength: 2048);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Monitors",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Response",
                table: "CheckLogs",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.CreateIndex(
                name: "IX_CheckLogs_MonitorId",
                table: "CheckLogs",
                column: "MonitorId");
        }
    }
}

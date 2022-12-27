using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoMonitoring.Migrations
{
    /// <inheritdoc />
    public partial class Relationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content",
                table: "Videos");

            migrationBuilder.AddColumn<string>(
                name: "ContentPath",
                table: "Videos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ServerId",
                table: "Videos",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Videos_ServerId",
                table: "Videos",
                column: "ServerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Servers_ServerId",
                table: "Videos",
                column: "ServerId",
                principalTable: "Servers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Servers_ServerId",
                table: "Videos");

            migrationBuilder.DropIndex(
                name: "IX_Videos_ServerId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "ContentPath",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "ServerId",
                table: "Videos");

            migrationBuilder.AddColumn<byte[]>(
                name: "Content",
                table: "Videos",
                type: "BLOB",
                nullable: true);
        }
    }
}

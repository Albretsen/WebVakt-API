﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebVakt_API.Migrations
{
    /// <inheritdoc />
    public partial class AddedTypeAndAttributesToMonitor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Attributes",
                table: "Monitors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Monitors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attributes",
                table: "Monitors");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Monitors");
        }
    }
}

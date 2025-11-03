using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarAssignment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCarParkingExitTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "ParkingExitTime",
                table: "Cars",
                type: "datetimeoffset",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParkingExitTime",
                table: "Cars");
        }
    }
}

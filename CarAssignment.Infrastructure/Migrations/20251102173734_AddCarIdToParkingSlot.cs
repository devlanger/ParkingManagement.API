using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarAssignment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCarIdToParkingSlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_ParkingSlots_AllocatedParkingSlotId",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_AllocatedParkingSlotId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "AllocatedParkingSlotId",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "CarId",
                table: "ParkingSlots",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParkingSlots_CarId",
                table: "ParkingSlots",
                column: "CarId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingSlots_Cars_CarId",
                table: "ParkingSlots",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingSlots_Cars_CarId",
                table: "ParkingSlots");

            migrationBuilder.DropIndex(
                name: "IX_ParkingSlots_CarId",
                table: "ParkingSlots");

            migrationBuilder.DropColumn(
                name: "CarId",
                table: "ParkingSlots");

            migrationBuilder.AddColumn<int>(
                name: "AllocatedParkingSlotId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_AllocatedParkingSlotId",
                table: "Cars",
                column: "AllocatedParkingSlotId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_ParkingSlots_AllocatedParkingSlotId",
                table: "Cars",
                column: "AllocatedParkingSlotId",
                principalTable: "ParkingSlots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

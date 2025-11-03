using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarAssignment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCarChargeAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ChargeAmount",
                table: "Cars",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargeAmount",
                table: "Cars");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarAssignment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddChargeAdditionalToCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ChargeAdditional",
                table: "Cars",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChargeAdditional",
                table: "Cars");
        }
    }
}

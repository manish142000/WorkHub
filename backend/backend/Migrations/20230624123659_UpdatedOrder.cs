using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OrderType",
                table: "Orders",
                newName: "Lunch");

            migrationBuilder.RenameColumn(
                name: "FoodType",
                table: "Orders",
                newName: "Breakfast");

            migrationBuilder.AddColumn<int>(
                name: "DayCreated",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DayCreated",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Lunch",
                table: "Orders",
                newName: "OrderType");

            migrationBuilder.RenameColumn(
                name: "Breakfast",
                table: "Orders",
                newName: "FoodType");
        }
    }
}

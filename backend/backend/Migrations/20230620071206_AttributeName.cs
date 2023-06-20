using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class AttributeName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_Email",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Orders",
                newName: "UserEmail");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_Email",
                table: "Orders",
                newName: "IX_Orders_UserEmail");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserEmail",
                table: "Orders",
                column: "UserEmail",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserEmail",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "Orders",
                newName: "Email");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_UserEmail",
                table: "Orders",
                newName: "IX_Orders_Email");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_Email",
                table: "Orders",
                column: "Email",
                principalTable: "Users",
                principalColumn: "Email",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

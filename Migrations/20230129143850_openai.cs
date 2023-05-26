using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Migrations
{
    /// <inheritdoc />
    public partial class openai : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LaptopTypes_Laptops_LaptopId",
                table: "LaptopTypes");

            migrationBuilder.DropIndex(
                name: "IX_LaptopTypes_LaptopId",
                table: "LaptopTypes");

            migrationBuilder.DropColumn(
                name: "LaptopId",
                table: "LaptopTypes");

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_LaptopsTypesId",
                table: "Laptops",
                column: "LaptopsTypesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Laptops_LaptopTypes_LaptopsTypesId",
                table: "Laptops",
                column: "LaptopsTypesId",
                principalTable: "LaptopTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laptops_LaptopTypes_LaptopsTypesId",
                table: "Laptops");

            migrationBuilder.DropIndex(
                name: "IX_Laptops_LaptopsTypesId",
                table: "Laptops");

            migrationBuilder.AddColumn<int>(
                name: "LaptopId",
                table: "LaptopTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LaptopTypes_LaptopId",
                table: "LaptopTypes",
                column: "LaptopId");

            migrationBuilder.AddForeignKey(
                name: "FK_LaptopTypes_Laptops_LaptopId",
                table: "LaptopTypes",
                column: "LaptopId",
                principalTable: "Laptops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Migrations
{
    /// <inheritdoc />
    public partial class ghjsjjfsaS223569 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Laptops_LaptopsType_LaptopsTypesID",
                table: "Laptops");

            migrationBuilder.DropTable(
                name: "LaptopsType");

            migrationBuilder.DropIndex(
                name: "IX_Laptops_LaptopsTypesID",
                table: "Laptops");

            migrationBuilder.DropColumn(
                name: "LaptopsTypesID",
                table: "Laptops");

            migrationBuilder.AddColumn<string>(
                name: "LaptopsTypes",
                table: "Laptops",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LaptopsTypes",
                table: "Laptops");

            migrationBuilder.AddColumn<int>(
                name: "LaptopsTypesID",
                table: "Laptops",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LaptopsType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaptopsType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Laptops_LaptopsTypesID",
                table: "Laptops",
                column: "LaptopsTypesID");

            migrationBuilder.AddForeignKey(
                name: "FK_Laptops_LaptopsType_LaptopsTypesID",
                table: "Laptops",
                column: "LaptopsTypesID",
                principalTable: "LaptopsType",
                principalColumn: "Id");
        }
    }
}

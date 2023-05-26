using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.Migrations
{
    /// <inheritdoc />
    public partial class shoppingcartitemnewmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Laptops_LaptopId",
                table: "ShoppingCartItems");

            migrationBuilder.AlterColumn<int>(
                name: "LaptopId",
                table: "ShoppingCartItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "ShoppingCartItemsNew",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LaptopId = table.Column<int>(type: "int", nullable: true),
                    ComponentId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItemsNew", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItemsNew_Components_ComponentId",
                        column: x => x.ComponentId,
                        principalTable: "Components",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ShoppingCartItemsNew_Laptops_LaptopId",
                        column: x => x.LaptopId,
                        principalTable: "Laptops",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItemsNew_ComponentId",
                table: "ShoppingCartItemsNew",
                column: "ComponentId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItemsNew_LaptopId",
                table: "ShoppingCartItemsNew",
                column: "LaptopId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Laptops_LaptopId",
                table: "ShoppingCartItems",
                column: "LaptopId",
                principalTable: "Laptops",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCartItems_Laptops_LaptopId",
                table: "ShoppingCartItems");

            migrationBuilder.DropTable(
                name: "ShoppingCartItemsNew");

            migrationBuilder.AlterColumn<int>(
                name: "LaptopId",
                table: "ShoppingCartItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCartItems_Laptops_LaptopId",
                table: "ShoppingCartItems",
                column: "LaptopId",
                principalTable: "Laptops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

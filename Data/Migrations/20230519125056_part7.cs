using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication5.Data.Migrations
{
    public partial class part7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Cart_Items_Shopping_CartsId",
                table: "Cart_Items",
                column: "Shopping_CartsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Items_Shopping_Carts_Shopping_CartsId",
                table: "Cart_Items",
                column: "Shopping_CartsId",
                principalTable: "Shopping_Carts",
                principalColumn: "Shopping_CartsId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Items_Shopping_Carts_Shopping_CartsId",
                table: "Cart_Items");

            migrationBuilder.DropIndex(
                name: "IX_Cart_Items_Shopping_CartsId",
                table: "Cart_Items");
        }
    }
}

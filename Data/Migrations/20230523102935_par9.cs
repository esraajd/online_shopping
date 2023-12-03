using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication5.Data.Migrations
{
    public partial class par9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "BankCardNumber",
                table: "ThirdBank",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "BankCardNumber",
                table: "SecondBank",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "BankCardNumber",
                table: "FirstBank",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BankCardNumber",
                table: "ThirdBank",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "BankCardNumber",
                table: "SecondBank",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "BankCardNumber",
                table: "FirstBank",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}

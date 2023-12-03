using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication5.Data.Migrations
{
    public partial class part8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FirstBank",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    BankCardNumber = table.Column<int>(nullable: false),
                    Balance = table.Column<double>(nullable: false),
                    Password = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirstBank", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SecondBank",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    BankCardNumber = table.Column<int>(nullable: false),
                    Balance = table.Column<double>(nullable: false),
                    Password = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecondBank", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ThirdBank",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    BankCardNumber = table.Column<int>(nullable: false),
                    Balance = table.Column<double>(nullable: false),
                    Password = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThirdBank", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FirstBank");

            migrationBuilder.DropTable(
                name: "SecondBank");

            migrationBuilder.DropTable(
                name: "ThirdBank");
        }
    }
}

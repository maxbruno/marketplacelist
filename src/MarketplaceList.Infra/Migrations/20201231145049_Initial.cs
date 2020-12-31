using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MarketplaceList.Infra.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "ShoppingList",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(1024)", maxLength: 1024, nullable: false),
                    CreateAt = table.Column<DateTime>(type: "DATETIME2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "VARCHAR(1024)", maxLength: 1024, nullable: false),
                    Qtd = table.Column<int>(type: "Int32", nullable: false),
                    ShoppingListId = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "DATETIME2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Item_ShoppingList_ShoppingListId",
                        column: x => x.ShoppingListId,
                        principalSchema: "dbo",
                        principalTable: "ShoppingList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "dbo",
                table: "ShoppingList",
                columns: new[] { "Id", "CreateAt", "Name" },
                values: new object[] { new Guid("43d450a4-a859-4fa6-9816-2d43836a1a13"), new DateTime(2020, 12, 31, 11, 50, 49, 100, DateTimeKind.Local).AddTicks(3408), "compras para o churrasco" });

            migrationBuilder.CreateIndex(
                name: "IX_Item_ShoppingListId",
                schema: "dbo",
                table: "Item",
                column: "ShoppingListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Item",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "ShoppingList",
                schema: "dbo");
        }
    }
}

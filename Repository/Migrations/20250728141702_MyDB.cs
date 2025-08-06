using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Migrations
{
    /// <inheritdoc />
    public partial class MyDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CATEGORY_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CATEGORY_NAME = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CATEGORY_ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    PasswordSalt = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    PRODUCT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRODUCT_NAME = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: true),
                    PRICE = table.Column<decimal>(type: "money", nullable: false),
                    CAREGORY_ID = table.Column<int>(type: "int", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nchar(200)", fixedLength: true, maxLength: 200, nullable: true),
                    IMG_URL = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.PRODUCT_ID);
                    table.ForeignKey(
                        name: "FK_Products_Categories",
                        column: x => x.CAREGORY_ID,
                        principalTable: "Categories",
                        principalColumn: "CATEGORY_ID");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    ORDER_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ORSER_DATE = table.Column<DateOnly>(type: "date", nullable: true),
                    ORDER_SUM = table.Column<decimal>(type: "money", nullable: true),
                    USER_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.ORDER_ID);
                    table.ForeignKey(
                        name: "FK_Orders_Users",
                        column: x => x.USER_ID,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "RATING",
                columns: table => new
                {
                    RATING_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HOST = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    METHOD = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    PATH = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    REFERER = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    USER_AGENT = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    USER_ID = table.Column<int>(type: "int", nullable: true),
                    Record_Date = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RATING", x => x.RATING_ID);
                    table.ForeignKey(
                        name: "FK_RATING_Users",
                        column: x => x.USER_ID,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "OrderItem",
                columns: table => new
                {
                    ORDER_ITEM_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRODUCT_ID = table.Column<int>(type: "int", nullable: true),
                    ORDER_ID = table.Column<int>(type: "int", nullable: true),
                    QUANTITY = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItem", x => x.ORDER_ITEM_ID);
                    table.ForeignKey(
                        name: "FK_OrderItem_Orders",
                        column: x => x.ORDER_ID,
                        principalTable: "Orders",
                        principalColumn: "ORDER_ID");
                    table.ForeignKey(
                        name: "FK_OrderItem_Products",
                        column: x => x.PRODUCT_ID,
                        principalTable: "Products",
                        principalColumn: "PRODUCT_ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_ORDER_ID",
                table: "OrderItem",
                column: "ORDER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItem_PRODUCT_ID",
                table: "OrderItem",
                column: "PRODUCT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_USER_ID",
                table: "Orders",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CAREGORY_ID",
                table: "Products",
                column: "CAREGORY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_RATING_USER_ID",
                table: "RATING",
                column: "USER_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItem");

            migrationBuilder.DropTable(
                name: "RATING");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}

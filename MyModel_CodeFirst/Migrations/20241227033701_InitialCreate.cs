using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyModel_CodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    BookID = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    SN = table.Column<long>(type: "bigint", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TimeStmp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhotoType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Photo = table.Column<string>(type: "nvarchar(44)", maxLength: 44, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.BookID);
                });

            migrationBuilder.CreateTable(
                name: "Rebook",
                columns: table => new
                {
                    ReBookID = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    SN = table.Column<long>(type: "bigint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    TimeStmp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookID = table.Column<string>(type: "nvarchar(36)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rebook", x => x.ReBookID);
                    table.ForeignKey(
                        name: "FK_Rebook_Book_BookID",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "BookID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rebook_BookID",
                table: "Rebook",
                column: "BookID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rebook");

            migrationBuilder.DropTable(
                name: "Book");
        }
    }
}

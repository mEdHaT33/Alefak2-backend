using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alefak2.Migrations
{
    /// <inheritdoc />
    public partial class Users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
            name: "users",
            columns: table => new
            {
                ID = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                Email = table.Column<string>(type: "varchar(100)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Password = table.Column<string>(type: "varchar(100)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                UserName = table.Column<string>(type: "varchar(100)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Phone = table.Column<string>(type: "varchar(100)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Country = table.Column<string>(type: "varchar(50)", nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                City = table.Column<string>(type: "varchar(50)", nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_users", x => x.ID);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

                migrationBuilder.CreateIndex(
                    name: "IX_users_UserName",
                    table: "users",
                    column: "UserName",
                    unique: true);

           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comments");

            migrationBuilder.DropTable(
                name: "likes");

            migrationBuilder.DropColumn(
                name: "City",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "users");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "users");

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "posts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

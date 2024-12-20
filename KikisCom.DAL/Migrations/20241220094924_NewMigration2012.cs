using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KikisCom.DAL.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration2012 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "lastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Mails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromMail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SMTPPatch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SMTPPort = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SMTPPassword = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mails", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mails");

            migrationBuilder.DropColumn(
                name: "lastName",
                table: "AspNetUsers");
        }
    }
}

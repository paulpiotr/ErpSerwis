using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ErpSerwis.Core.Migrations
{
    public partial class _1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "esc");

            migrationBuilder.CreateTable(
                name: "Students",
                schema: "esc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    Name = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    Surname = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    IndexNumber = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsDateOfBirth",
                schema: "esc",
                table: "Students",
                column: "DateOfBirth");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsId",
                schema: "esc",
                table: "Students",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentsIndexNumber",
                schema: "esc",
                table: "Students",
                column: "IndexNumber");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsName",
                schema: "esc",
                table: "Students",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsSurname",
                schema: "esc",
                table: "Students",
                column: "Surname");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students",
                schema: "esc");
        }
    }
}

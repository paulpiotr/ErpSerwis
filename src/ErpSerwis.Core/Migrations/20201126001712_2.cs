using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ErpSerwis.Core.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentsGrades",
                schema: "esc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newsequentialid())"),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Grade = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsGrades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentsGrades_Students_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "esc",
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StudentsGradesId",
                schema: "esc",
                table: "StudentsGrades",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentsGradesStudentId",
                schema: "esc",
                table: "StudentsGrades",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentsGrades",
                schema: "esc");
        }
    }
}

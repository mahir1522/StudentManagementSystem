using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StudentManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class initials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Instuctor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoomNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GetNumberOfStudents = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    S_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.S_Id);
                    table.ForeignKey(
                        name: "FK_Students_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Date", "GetNumberOfStudents", "Instuctor", "Name", "RoomNumber" },
                values: new object[,]
                {
                    { 1, new DateTime(2004, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Holl", "Test", "2G12" },
                    { 2, new DateTime(1984, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "Moll", "Test2", "4F31" },
                    { 3, new DateTime(2020, 9, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, "helo", "test3", "5G12" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "S_Id", "CourseId", "Email", "Name", "Status" },
                values: new object[,]
                {
                    { 1, 1, "john@example.com", "John Mohn", 0 },
                    { 2, 2, "Smithwill@example.com", "Smith will", 0 },
                    { 3, 3, "Testing@gmail.com", "Test3", 0 },
                    { 4, 3, "Test4@gmail.com", "test4", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_CourseId",
                table: "Students",
                column: "CourseId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Courses");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechTest.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Record",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    caller_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    recipient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    call_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    duration = table.Column<int>(type: "int", nullable: false),
                    cost = table.Column<double>(type: "float", nullable: false),
                    reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    currency = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Record", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Record");
        }
    }
}

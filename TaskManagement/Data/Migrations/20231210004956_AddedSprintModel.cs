using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManagement.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedSprintModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sprints",
                columns: table => new
                {
                    SprintId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SprintName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SprintStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SprintEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BacklogId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprints", x => x.SprintId);
                    table.ForeignKey(
                        name: "FK_Sprints_Backlogs_BacklogId",
                        column: x => x.BacklogId,
                        principalTable: "Backlogs",
                        principalColumn: "BacklogId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sprints_BacklogId",
                table: "Sprints",
                column: "BacklogId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sprints");
        }
    }
}

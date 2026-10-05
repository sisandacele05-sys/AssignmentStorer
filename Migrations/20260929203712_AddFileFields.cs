using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssignmentStorer.Migrations
{
    /// <inheritdoc />
    public partial class AddFileFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Assignments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Assignments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Assignments");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Assignments");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssignmentStorer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAssignmentFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "Assignments",
                newName: "Module");

            migrationBuilder.AddColumn<DateTime>(
                name: "UploadDate",
                table: "Assignments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UploadDate",
                table: "Assignments");

            migrationBuilder.RenameColumn(
                name: "Module",
                table: "Assignments",
                newName: "Subject");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Assignments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

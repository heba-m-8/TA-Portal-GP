using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TAManagment.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedNoteAttributes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeanNote",
                table: "WorkRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeanOfGraduateStudiesNote",
                table: "WorkRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FinanceNote",
                table: "WorkRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HODnote",
                table: "WorkRecord",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "InstructorNote",
                table: "WorkRecord",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeanNote",
                table: "WorkRecord");

            migrationBuilder.DropColumn(
                name: "DeanOfGraduateStudiesNote",
                table: "WorkRecord");

            migrationBuilder.DropColumn(
                name: "FinanceNote",
                table: "WorkRecord");

            migrationBuilder.DropColumn(
                name: "HODnote",
                table: "WorkRecord");

            migrationBuilder.DropColumn(
                name: "InstructorNote",
                table: "WorkRecord");
        }
    }
}

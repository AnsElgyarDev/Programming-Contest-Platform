using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Programming_Contest_Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddTestCasesAndJudgeDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CompilerOutput",
                table: "Submissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExecutionTimeMs",
                table: "Submissions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MemoryUsedKB",
                table: "Submissions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompilerOutput",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "ExecutionTimeMs",
                table: "Submissions");

            migrationBuilder.DropColumn(
                name: "MemoryUsedKB",
                table: "Submissions");
        }
    }
}

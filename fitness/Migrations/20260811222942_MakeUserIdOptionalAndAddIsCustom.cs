using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace fitness.Migrations
{
    /// <inheritdoc />
    public partial class MakeUserIdOptionalAndAddIsCustom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsCustom",
                table: "Workouts",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCustom",
                table: "Workouts");
        }
    }
}

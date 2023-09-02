using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Database.Migrations
{
    /// <inheritdoc />
    public partial class RemoveDataTypeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "data_type",
                table: "log");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "data_type",
                table: "log",
                type: "varchar(250)",
                maxLength: 250,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}

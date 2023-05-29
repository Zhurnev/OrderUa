using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderUA.Migrations
{
    /// <inheritdoc />
    public partial class AddRequiredFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.AlterColumn<string>(
				name: "Name",
				table: "Category",
				type: "nvarchar(max)",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(max)",
				oldNullable: true);

			migrationBuilder.AlterColumn<string>(
				name: "Name",
				table: "ProductType",
				type: "nvarchar(max)",
				nullable: false,
				oldClrType: typeof(string),
				oldType: "nvarchar(max)",
				oldNullable: true);
		}

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductType");
        }
    }
}

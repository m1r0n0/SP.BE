using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SP.Provider.DataAccessLayer.Migrations
{
    public partial class RenameProviderIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Providers",
                newName: "ProviderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProviderId",
                table: "Providers",
                newName: "Id");
        }
    }
}

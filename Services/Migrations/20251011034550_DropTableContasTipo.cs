using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceiroBackend.Migrations
{
    /// <inheritdoc />
    public partial class DropTableContasTipo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contas_ContaTipos_ContaTipoId",
                table: "Contas");

            migrationBuilder.DropTable(
                name: "ContaTipos");

            migrationBuilder.DropIndex(
                name: "IX_Contas_ContaTipoId",
                table: "Contas");

            migrationBuilder.DropColumn(
                name: "ContaTipoId",
                table: "Contas");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContaTipoId",
                table: "Contas",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ContaTipos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContaTipos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contas_ContaTipoId",
                table: "Contas",
                column: "ContaTipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contas_ContaTipos_ContaTipoId",
                table: "Contas",
                column: "ContaTipoId",
                principalTable: "ContaTipos",
                principalColumn: "Id");
        }
    }
}

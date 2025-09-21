using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinanceiroBackend.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableContas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contas_ContaTipos_TipoId",
                table: "Contas");

            migrationBuilder.DropIndex(
                name: "IX_Contas_TipoId",
                table: "Contas");

            migrationBuilder.DropColumn(
                name: "DataCriacao",
                table: "Contas");

            migrationBuilder.RenameColumn(
                name: "Valor",
                table: "Contas",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "TipoId",
                table: "Contas",
                newName: "Type");

            migrationBuilder.AddColumn<string>(
                name: "ContaTipoId",
                table: "Contas",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Contas",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Contas",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Mouth",
                table: "Contas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Year",
                table: "Contas",
                type: "integer",
                nullable: false,
                defaultValue: 0);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contas_ContaTipos_ContaTipoId",
                table: "Contas");

            migrationBuilder.DropIndex(
                name: "IX_Contas_ContaTipoId",
                table: "Contas");

            migrationBuilder.DropColumn(
                name: "ContaTipoId",
                table: "Contas");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Contas");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Contas");

            migrationBuilder.DropColumn(
                name: "Mouth",
                table: "Contas");

            migrationBuilder.DropColumn(
                name: "Year",
                table: "Contas");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Contas",
                newName: "TipoId");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Contas",
                newName: "Valor");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCriacao",
                table: "Contas",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Contas_TipoId",
                table: "Contas",
                column: "TipoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contas_ContaTipos_TipoId",
                table: "Contas",
                column: "TipoId",
                principalTable: "ContaTipos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

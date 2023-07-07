using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiTeste.Migrations
{
    /// <inheritdoc />
    public partial class ChangingNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Pessoas",
                table: "Pessoas");

            migrationBuilder.RenameTable(
                name: "Pessoas",
                newName: "tb_pessoas");

            migrationBuilder.RenameColumn(
                name: "Salario",
                table: "tb_pessoas",
                newName: "salario");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "tb_pessoas",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Cidade",
                table: "tb_pessoas",
                newName: "cidade");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tb_pessoas",
                newName: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_pessoas",
                table: "tb_pessoas",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_pessoas",
                table: "tb_pessoas");

            migrationBuilder.RenameTable(
                name: "tb_pessoas",
                newName: "Pessoas");

            migrationBuilder.RenameColumn(
                name: "salario",
                table: "Pessoas",
                newName: "Salario");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Pessoas",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "cidade",
                table: "Pessoas",
                newName: "Cidade");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Pessoas",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Pessoas",
                table: "Pessoas",
                column: "Id");
        }
    }
}

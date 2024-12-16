using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AlteradoNomesTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contato_AreaSet_AreaId",
                table: "Contato");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contato",
                table: "Contato");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AreaSet",
                table: "AreaSet");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "Contato",
                newName: "Contatos");

            migrationBuilder.RenameTable(
                name: "AreaSet",
                newName: "Areas");

            migrationBuilder.RenameIndex(
                name: "IX_Contato_AreaId",
                table: "Contatos",
                newName: "IX_Contatos_AreaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contatos",
                table: "Contatos",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Areas",
                table: "Areas",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contatos_Areas_AreaId",
                table: "Contatos",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contatos_Areas_AreaId",
                table: "Contatos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contatos",
                table: "Contatos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Areas",
                table: "Areas");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");

            migrationBuilder.RenameTable(
                name: "Contatos",
                newName: "Contato");

            migrationBuilder.RenameTable(
                name: "Areas",
                newName: "AreaSet");

            migrationBuilder.RenameIndex(
                name: "IX_Contatos_AreaId",
                table: "Contato",
                newName: "IX_Contato_AreaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contato",
                table: "Contato",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AreaSet",
                table: "AreaSet",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Contato_AreaSet_AreaId",
                table: "Contato",
                column: "AreaId",
                principalTable: "AreaSet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

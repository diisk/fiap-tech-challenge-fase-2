using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RelacaoContatoAreaAlterada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contatos_Areas_AreaId",
                table: "Contatos");

            migrationBuilder.RenameColumn(
                name: "AreaId",
                table: "Contatos",
                newName: "CodigoArea");

            migrationBuilder.RenameIndex(
                name: "IX_Contatos_AreaId",
                table: "Contatos",
                newName: "IX_Contatos_CodigoArea");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Areas_Codigo",
                table: "Areas",
                column: "Codigo");

            migrationBuilder.CreateIndex(
                name: "IX_Areas_Codigo",
                table: "Areas",
                column: "Codigo");

            migrationBuilder.AddForeignKey(
                name: "FK_Contatos_Areas_CodigoArea",
                table: "Contatos",
                column: "CodigoArea",
                principalTable: "Areas",
                principalColumn: "Codigo",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contatos_Areas_CodigoArea",
                table: "Contatos");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Areas_Codigo",
                table: "Areas");

            migrationBuilder.DropIndex(
                name: "IX_Areas_Codigo",
                table: "Areas");

            migrationBuilder.RenameColumn(
                name: "CodigoArea",
                table: "Contatos",
                newName: "AreaId");

            migrationBuilder.RenameIndex(
                name: "IX_Contatos_CodigoArea",
                table: "Contatos",
                newName: "IX_Contatos_AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contatos_Areas_AreaId",
                table: "Contatos",
                column: "AreaId",
                principalTable: "Areas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

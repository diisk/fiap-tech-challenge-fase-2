using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoRelacaoContatoArea : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AreaSet_Contato_Codigo",
                table: "AreaSet");

            migrationBuilder.DropIndex(
                name: "IX_AreaSet_Codigo",
                table: "AreaSet");

            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "Contato",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Contato_AreaId",
                table: "Contato",
                column: "AreaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contato_AreaSet_AreaId",
                table: "Contato",
                column: "AreaId",
                principalTable: "AreaSet",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contato_AreaSet_AreaId",
                table: "Contato");

            migrationBuilder.DropIndex(
                name: "IX_Contato_AreaId",
                table: "Contato");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "Contato");

            migrationBuilder.CreateIndex(
                name: "IX_AreaSet_Codigo",
                table: "AreaSet",
                column: "Codigo",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AreaSet_Contato_Codigo",
                table: "AreaSet",
                column: "Codigo",
                principalTable: "Contato",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

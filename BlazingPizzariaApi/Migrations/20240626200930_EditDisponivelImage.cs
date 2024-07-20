using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazingPizza.Api.Migrations
{
    /// <inheritdoc />
    public partial class EditDisponivelImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Disponibilidades_DisponibilidadeId",
                table: "Produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Imagem_ImagemId",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "Imagem");

            migrationBuilder.DropColumn(
                name: "ProdutoId",
                table: "Disponibilidades");

            migrationBuilder.AlterColumn<int>(
                name: "ImagemId",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DisponibilidadeId",
                table: "Produtos",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Avaliacaos_RevisaoId",
                table: "Avaliacaos",
                column: "RevisaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Avaliacaos_Revisaos_RevisaoId",
                table: "Avaliacaos",
                column: "RevisaoId",
                principalTable: "Revisaos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Disponibilidades_DisponibilidadeId",
                table: "Produtos",
                column: "DisponibilidadeId",
                principalTable: "Disponibilidades",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Imagem_ImagemId",
                table: "Produtos",
                column: "ImagemId",
                principalTable: "Imagem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Avaliacaos_Revisaos_RevisaoId",
                table: "Avaliacaos");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Disponibilidades_DisponibilidadeId",
                table: "Produtos");

            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Imagem_ImagemId",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Avaliacaos_RevisaoId",
                table: "Avaliacaos");

            migrationBuilder.AlterColumn<int>(
                name: "ImagemId",
                table: "Produtos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "DisponibilidadeId",
                table: "Produtos",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProdutoId",
                table: "Imagem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProdutoId",
                table: "Disponibilidades",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Disponibilidades_DisponibilidadeId",
                table: "Produtos",
                column: "DisponibilidadeId",
                principalTable: "Disponibilidades",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Imagem_ImagemId",
                table: "Produtos",
                column: "ImagemId",
                principalTable: "Imagem",
                principalColumn: "Id");
        }
    }
}

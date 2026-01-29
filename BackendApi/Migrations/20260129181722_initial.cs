using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackendApi.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Celular = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    TentativasLogin = table.Column<int>(type: "int", nullable: false),
                    LoginBloqueado = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Apiarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Localizacao_Rua = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Localizacao_Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Localizacao_Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Localizacao_Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Localizacao_DescricaoLocal = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Localizacao_Referencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Coord_X = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Coord_Y = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bioma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoDeAbelha = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoDeMel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Atividade = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apiarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Apiarios_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Colmeias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiarioId = table.Column<int>(type: "int", nullable: false),
                    AnoColmeia = table.Column<int>(type: "int", nullable: false),
                    AnoRainha = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colmeias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colmeias_Apiarios_ApiarioId",
                        column: x => x.ApiarioId,
                        principalTable: "Apiarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProducaoApiarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiarioId = table.Column<int>(type: "int", nullable: false),
                    TotalProduzidoKg = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    TotalVendido = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    EstoqueAtualKg = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProducaoApiarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProducaoApiarios_Apiarios_ApiarioId",
                        column: x => x.ApiarioId,
                        principalTable: "Apiarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovimentacaoMel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiarioId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    RetiradoDaColmeiaId = table.Column<int>(type: "int", nullable: true),
                    QuantidadeKg = table.Column<decimal>(type: "decimal(18,3)", precision: 18, scale: 3, nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Data = table.Column<DateOnly>(type: "date", nullable: false),
                    Motivo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletionDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentacaoMel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimentacaoMel_Apiarios_ApiarioId",
                        column: x => x.ApiarioId,
                        principalTable: "Apiarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovimentacaoMel_Colmeias_RetiradoDaColmeiaId",
                        column: x => x.RetiradoDaColmeiaId,
                        principalTable: "Colmeias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Apiarios_UserId",
                table: "Apiarios",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Colmeias_ApiarioId",
                table: "Colmeias",
                column: "ApiarioId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacaoMel_ApiarioId",
                table: "MovimentacaoMel",
                column: "ApiarioId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentacaoMel_RetiradoDaColmeiaId",
                table: "MovimentacaoMel",
                column: "RetiradoDaColmeiaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProducaoApiarios_ApiarioId",
                table: "ProducaoApiarios",
                column: "ApiarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimentacaoMel");

            migrationBuilder.DropTable(
                name: "ProducaoApiarios");

            migrationBuilder.DropTable(
                name: "Colmeias");

            migrationBuilder.DropTable(
                name: "Apiarios");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

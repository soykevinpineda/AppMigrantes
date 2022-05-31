using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrantes.Migrations
{
    public partial class AgregandoModelo_Fiador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FiadorId_Fiador",
                schema: "mig",
                table: "ide_identidad",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "fiador",
                schema: "mig",
                columns: table => new
                {
                    Id_Fiador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    per_codigo_id = table.Column<int>(type: "int", nullable: false),
                    primer_nombre_Fiador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    segundo_nombre_Fiador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    primer_apellido_Fiador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    segundo_apellido_Fiador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pais_nacimiento_Fiador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    profesion_Fiador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sexo_Fiador = table.Column<int>(type: "int", nullable: false),
                    edad_Fiador = table.Column<int>(type: "int", nullable: false),
                    email_Fiador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    telefono_Fiador = table.Column<int>(type: "int", nullable: false),
                    telefono_alterno_Fiador = table.Column<int>(type: "int", nullable: false),
                    fecha_grabacion_Fiador = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fiador", x => x.Id_Fiador);
                    table.ForeignKey(
                        name: "FK_fiador_per_persona_per_codigo_id",
                        column: x => x.per_codigo_id,
                        principalSchema: "mig",
                        principalTable: "per_persona",
                        principalColumn: "per_codigo_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ide_identidad_FiadorId_Fiador",
                schema: "mig",
                table: "ide_identidad",
                column: "FiadorId_Fiador");

            migrationBuilder.CreateIndex(
                name: "IX_fiador_per_codigo_id",
                schema: "mig",
                table: "fiador",
                column: "per_codigo_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ide_identidad_fiador_FiadorId_Fiador",
                schema: "mig",
                table: "ide_identidad",
                column: "FiadorId_Fiador",
                principalSchema: "mig",
                principalTable: "fiador",
                principalColumn: "Id_Fiador",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ide_identidad_fiador_FiadorId_Fiador",
                schema: "mig",
                table: "ide_identidad");

            migrationBuilder.DropTable(
                name: "fiador",
                schema: "mig");

            migrationBuilder.DropIndex(
                name: "IX_ide_identidad_FiadorId_Fiador",
                schema: "mig",
                table: "ide_identidad");

            migrationBuilder.DropColumn(
                name: "FiadorId_Fiador",
                schema: "mig",
                table: "ide_identidad");
        }
    }
}

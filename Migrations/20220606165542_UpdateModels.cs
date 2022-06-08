using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrantes.Migrations
{
    public partial class UpdateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RutaImagen",
                schema: "mig",
                table: "ide_identidad",
                newName: "RutaImagenPortada_B");

            migrationBuilder.RenameColumn(
                name: "NombreImagen",
                schema: "mig",
                table: "ide_identidad",
                newName: "NombreImagenPortada_B");

            migrationBuilder.AddColumn<string>(
                name: "NombreImagenPortada_A",
                schema: "mig",
                table: "ide_identidad",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RutaImagenPortada_A",
                schema: "mig",
                table: "ide_identidad",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "datos_familiares",
                schema: "mig",
                columns: table => new
                {
                    id_datos_familiares = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    per_codigo_id = table.Column<int>(type: "int", nullable: false),
                    nombres_madre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    primer_apellido_madre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    segundo_apellido_madre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    edad_madre = table.Column<int>(type: "int", nullable: false),
                    profesion_madre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    nombres_padre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    primer_apellido_padre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    segundo_apellido_padre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    edad_padre = table.Column<int>(type: "int", nullable: false),
                    profesion_padre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    estado_datosfamiliares = table.Column<int>(type: "int", nullable: false),
                    PersonaLinkper_codigo_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_datos_familiares", x => x.id_datos_familiares);
                    table.ForeignKey(
                        name: "FK_datos_familiares_per_persona_PersonaLinkper_codigo_id",
                        column: x => x.PersonaLinkper_codigo_id,
                        principalSchema: "mig",
                        principalTable: "per_persona",
                        principalColumn: "per_codigo_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ModeloFiador",
                schema: "mig",
                columns: table => new
                {
                    IdFiador = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    per_codigo_id = table.Column<int>(type: "int", nullable: false),
                    PrimerNombreFiador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SegundoNombreFiador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PrimerApellidoFiador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SegundoApellidoFiador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaisNacimientoFiador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EdadFiador = table.Column<int>(type: "int", nullable: false),
                    SexoFiador = table.Column<int>(type: "int", nullable: false),
                    EmailFiador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelefonoFiador = table.Column<int>(type: "int", nullable: false),
                    TelefonoAlternoFiador = table.Column<int>(type: "int", nullable: false),
                    NumCartasPersonales = table.Column<int>(type: "int", nullable: false),
                    NumCartasFamiliares = table.Column<int>(type: "int", nullable: false),
                    EntregoRecibo_Agua_o_Luz = table.Column<bool>(type: "bit", nullable: false),
                    FechaGrabacionFiador = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeloFiador", x => x.IdFiador);
                    table.ForeignKey(
                        name: "FK_ModeloFiador_per_persona_per_codigo_id",
                        column: x => x.per_codigo_id,
                        principalSchema: "mig",
                        principalTable: "per_persona",
                        principalColumn: "per_codigo_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_datos_familiares_PersonaLinkper_codigo_id",
                schema: "mig",
                table: "datos_familiares",
                column: "PersonaLinkper_codigo_id");

            migrationBuilder.CreateIndex(
                name: "IX_ModeloFiador_per_codigo_id",
                schema: "mig",
                table: "ModeloFiador",
                column: "per_codigo_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "datos_familiares",
                schema: "mig");

            migrationBuilder.DropTable(
                name: "ModeloFiador",
                schema: "mig");

            migrationBuilder.DropColumn(
                name: "NombreImagenPortada_A",
                schema: "mig",
                table: "ide_identidad");

            migrationBuilder.DropColumn(
                name: "RutaImagenPortada_A",
                schema: "mig",
                table: "ide_identidad");

            migrationBuilder.RenameColumn(
                name: "RutaImagenPortada_B",
                schema: "mig",
                table: "ide_identidad",
                newName: "RutaImagen");

            migrationBuilder.RenameColumn(
                name: "NombreImagenPortada_B",
                schema: "mig",
                table: "ide_identidad",
                newName: "NombreImagen");
        }
    }
}

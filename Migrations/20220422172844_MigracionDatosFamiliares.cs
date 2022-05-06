using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrantes.Migrations
{
    public partial class MigracionDatosFamiliares : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_datos_familiares_PersonaLinkper_codigo_id",
                schema: "mig",
                table: "datos_familiares",
                column: "PersonaLinkper_codigo_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "datos_familiares",
                schema: "mig");
        }
    }
}

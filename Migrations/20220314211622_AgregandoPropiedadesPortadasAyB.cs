using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrantes.Migrations
{
    public partial class AgregandoPropiedadesPortadasAyB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RutaImagen",
                schema: "mig",
                table: "ide_identidad",
                newName: "RutaImagenPortada_A");

            migrationBuilder.RenameColumn(
                name: "NombreImagen",
                schema: "mig",
                table: "ide_identidad",
                newName: "NombreImagenPortada_A");

            migrationBuilder.AddColumn<string>(
                name: "NombreImagenPortada_B",
                schema: "mig",
                table: "ide_identidad",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RutaImagenPortada_B",
                schema: "mig",
                table: "ide_identidad",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NombreImagenPortada_B",
                schema: "mig",
                table: "ide_identidad");

            migrationBuilder.DropColumn(
                name: "RutaImagenPortada_B",
                schema: "mig",
                table: "ide_identidad");

            migrationBuilder.RenameColumn(
                name: "RutaImagenPortada_A",
                schema: "mig",
                table: "ide_identidad",
                newName: "RutaImagen");

            migrationBuilder.RenameColumn(
                name: "NombreImagenPortada_A",
                schema: "mig",
                table: "ide_identidad",
                newName: "NombreImagen");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Migrantes.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "mig");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "estado_civil",
                schema: "mig",
                columns: table => new
                {
                    id_estado_civil = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    estado_civil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    activo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_estado_civil", x => x.id_estado_civil);
                });

            migrationBuilder.CreateTable(
                name: "parientes",
                schema: "mig",
                columns: table => new
                {
                    ParienteID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescripcionPariente = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_parientes", x => x.ParienteID);
                });

            migrationBuilder.CreateTable(
                name: "per_persona",
                schema: "mig",
                columns: table => new
                {
                    per_codigo_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    per_primer_nom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    per_segundo_nom = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    per_primer_ape = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    per_segundo_ape = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    per_apellido_cas = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    per_fecha_nac = table.Column<DateTime>(type: "datetime2", nullable: false),
                    per_edad = table.Column<int>(type: "int", nullable: false),
                    per_sexo = table.Column<int>(type: "int", nullable: false),
                    per_codpai_nacimiento = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    per_codpai_nacionalidad = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    per_profesion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    per_estado_civil = table.Column<int>(type: "int", nullable: false),
                    per_email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    per_codmun_nac = table.Column<int>(type: "int", nullable: false),
                    per_coddep_nac = table.Column<int>(type: "int", nullable: false),
                    per_lugar_nac = table.Column<int>(type: "int", nullable: false),
                    per_email_interno = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    per_telefono_movil = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    per_telefono_interno = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    per_observaciones = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    per_estado = table.Column<int>(type: "int", nullable: false),
                    per_codigo_alternativo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    per_letra_indice = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    per_codpai_digita = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    per_usuario_grabacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    per_fecha_grabacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    per_usuario_modificacion = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    per_fecha_modificacion = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_per_persona", x => x.per_codigo_id);
                });

            migrationBuilder.CreateTable(
                name: "sexo",
                schema: "mig",
                columns: table => new
                {
                    id_sexo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre_sexo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    nomenclatura_sexo = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    activo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sexo", x => x.id_sexo);
                });

            migrationBuilder.CreateTable(
                name: "tep_tipo_estado",
                schema: "mig",
                columns: table => new
                {
                    tep_tipo_estado = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tep_descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    tep_activo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tep_tipo_estado", x => x.tep_tipo_estado);
                });

            migrationBuilder.CreateTable(
                name: "tid_tipo_documento",
                schema: "mig",
                columns: table => new
                {
                    tid_id_documento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tid_descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    tid_activo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tid_tipo_documento", x => x.tid_id_documento);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "datos_familiares",
                schema: "mig",
                columns: table => new
                {
                    DatosFamiliaresID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    per_codigo_id = table.Column<int>(type: "int", nullable: false),
                    ParienteID = table.Column<int>(type: "int", nullable: false),
                    PrimerNombreFamiliar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SegundoNombreFamiliar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidosFamiliar = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimientoDelFamiliar = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaisNacimientoDelFamiliar = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EdadDelFamiliar = table.Column<int>(type: "int", nullable: false),
                    ProfesionDelFamiliar = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: true),
                    TelefonoDelFamiliar = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    TelefonoAlternativoFamiliar = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    EmaiDelFamiliar = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FechaGrabacionDelFamiliar = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EstadoDatosFamiliares = table.Column<int>(type: "int", nullable: false),
                    ParientesLinkParienteID = table.Column<int>(type: "int", nullable: true),
                    PersonaLinkper_codigo_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_datos_familiares", x => x.DatosFamiliaresID);
                    table.ForeignKey(
                        name: "FK_datos_familiares_parientes_ParientesLinkParienteID",
                        column: x => x.ParientesLinkParienteID,
                        principalSchema: "mig",
                        principalTable: "parientes",
                        principalColumn: "ParienteID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_datos_familiares_per_persona_PersonaLinkper_codigo_id",
                        column: x => x.PersonaLinkper_codigo_id,
                        principalSchema: "mig",
                        principalTable: "per_persona",
                        principalColumn: "per_codigo_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "fiador",
                schema: "mig",
                columns: table => new
                {
                    FiadorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    per_codigo_id = table.Column<int>(type: "int", nullable: false),
                    PrimerNombreDelFiador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SegundoNombreDelFiador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApellidosDelFiador = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimientoDelFiador = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EdadDelFiador = table.Column<int>(type: "int", nullable: false),
                    SexoDelFiador = table.Column<int>(type: "int", nullable: false),
                    PaisNacimientoDelFiador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailFiador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelefonoFiador = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    TelefonoAlternoFiador = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    NumCartasPersonales = table.Column<int>(type: "int", nullable: false),
                    NumCartasFamiliares = table.Column<int>(type: "int", nullable: false),
                    EntregoRecibo_Agua_o_Luz = table.Column<bool>(type: "bit", nullable: false),
                    FechaGrabacionDelFiador = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fiador", x => x.FiadorID);
                    table.ForeignKey(
                        name: "FK_fiador_per_persona_per_codigo_id",
                        column: x => x.per_codigo_id,
                        principalSchema: "mig",
                        principalTable: "per_persona",
                        principalColumn: "per_codigo_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ide_identidad",
                schema: "mig",
                columns: table => new
                {
                    ide_id_persona = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ide_codigo_id = table.Column<int>(type: "int", nullable: false),
                    ide_id_documento = table.Column<int>(type: "int", nullable: false),
                    ide_numero = table.Column<int>(type: "int", nullable: false),
                    ide_fecha_emision = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ide_fecha_vencimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ide_estado = table.Column<int>(type: "int", nullable: false),
                    ide_entregado = table.Column<bool>(type: "bit", nullable: false),
                    PersonaLinkper_codigo_id = table.Column<int>(type: "int", nullable: true),
                    TipoDocumentoLinktid_id_documento = table.Column<int>(type: "int", nullable: true),
                    NombreImagenPortada_A = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    RutaImagenPortada_A = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NombreImagenPortada_B = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    RutaImagenPortada_B = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ide_identidad", x => x.ide_id_persona);
                    table.ForeignKey(
                        name: "FK_ide_identidad_per_persona_PersonaLinkper_codigo_id",
                        column: x => x.PersonaLinkper_codigo_id,
                        principalSchema: "mig",
                        principalTable: "per_persona",
                        principalColumn: "per_codigo_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ide_identidad_tid_tipo_documento_TipoDocumentoLinktid_id_documento",
                        column: x => x.TipoDocumentoLinktid_id_documento,
                        principalSchema: "mig",
                        principalTable: "tid_tipo_documento",
                        principalColumn: "tid_id_documento",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_datos_familiares_ParientesLinkParienteID",
                schema: "mig",
                table: "datos_familiares",
                column: "ParientesLinkParienteID");

            migrationBuilder.CreateIndex(
                name: "IX_datos_familiares_PersonaLinkper_codigo_id",
                schema: "mig",
                table: "datos_familiares",
                column: "PersonaLinkper_codigo_id");

            migrationBuilder.CreateIndex(
                name: "IX_fiador_per_codigo_id",
                schema: "mig",
                table: "fiador",
                column: "per_codigo_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ide_identidad_PersonaLinkper_codigo_id",
                schema: "mig",
                table: "ide_identidad",
                column: "PersonaLinkper_codigo_id");

            migrationBuilder.CreateIndex(
                name: "IX_ide_identidad_TipoDocumentoLinktid_id_documento",
                schema: "mig",
                table: "ide_identidad",
                column: "TipoDocumentoLinktid_id_documento");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "datos_familiares",
                schema: "mig");

            migrationBuilder.DropTable(
                name: "estado_civil",
                schema: "mig");

            migrationBuilder.DropTable(
                name: "fiador",
                schema: "mig");

            migrationBuilder.DropTable(
                name: "ide_identidad",
                schema: "mig");

            migrationBuilder.DropTable(
                name: "sexo",
                schema: "mig");

            migrationBuilder.DropTable(
                name: "tep_tipo_estado",
                schema: "mig");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "parientes",
                schema: "mig");

            migrationBuilder.DropTable(
                name: "per_persona",
                schema: "mig");

            migrationBuilder.DropTable(
                name: "tid_tipo_documento",
                schema: "mig");
        }
    }
}

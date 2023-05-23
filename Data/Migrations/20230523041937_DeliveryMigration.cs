using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace appPetech.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeliveryMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_delivery",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    nombre = table.Column<string>(type: "text", nullable: false),
                    apepat = table.Column<string>(type: "text", nullable: false),
                    apemat = table.Column<string>(type: "text", nullable: false),
                    dni = table.Column<string>(type: "text", nullable: false),
                    celular = table.Column<string>(type: "text", nullable: false),
                    vehiculo = table.Column<string>(type: "text", nullable: false),
                    placa = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_delivery", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_delivery");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace appPetech.Data.Migrations
{
    /// <inheritdoc />
    public partial class Cart2Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "t_cart",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserID = table.Column<string>(type: "text", nullable: true),
                    ProductoId = table.Column<int>(type: "integer", nullable: true),
                    Cantidad = table.Column<int>(type: "integer", nullable: false),
                    Precio = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_cart", x => x.id);
                    table.ForeignKey(
                        name: "FK_t_cart_t_producto_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "t_producto",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_t_cart_ProductoId",
                table: "t_cart",
                column: "ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "t_cart");
        }
    }
}

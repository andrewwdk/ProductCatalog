using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductCatalog.Migrations
{
    /// <inheritdoc />
    public partial class AddGetTopNProductsProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
               CREATE PROCEDURE GetTopNProducts
                    @Count INT
                AS
                BEGIN
                    SELECT TOP (@Count) *
                    FROM Products
                    ORDER BY Price DESC;
                END;
           ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS GetTopNProducts;");
        }
    }
}

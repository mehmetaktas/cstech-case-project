using CicekSepetiTech.Case.Data.DbEntity;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace CicekSepetiTech.Case.Data.Migrations
{
    public partial class InitTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(name: nameof(Customer), columns: table => new
            {
                Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                FirstName = table.Column<string>(),
                LastName = table.Column<string>(),
                Email = table.Column<string>(),
                Deleted = table.Column<bool>(),
                CreateDate = table.Column<DateTime>(),
                UpdateDate = table.Column<DateTime>(),
            },
           constraints: table =>
           {
               table.PrimaryKey($"PK_{nameof(Customer)}", x => x.Id);
           });

            migrationBuilder.CreateTable(name: nameof(ProductAvailability), columns: table => new
            {
                Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                Name = table.Column<string>(),
                Display = table.Column<bool>(),
                Salable = table.Column<bool>(),
                Deleted = table.Column<bool>(),
                CreateDate = table.Column<DateTime>()
            },
          constraints: table =>
          {
              table.PrimaryKey($"PK_{nameof(ProductAvailability)}", x => x.Id);
          });

            migrationBuilder.CreateTable(name: nameof(Product), columns: table => new
            {
                Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                Code = table.Column<string>(),
                Name = table.Column<string>(),
                PriceInclTax = table.Column<decimal>(),
                SalableQuantity = table.Column<int>(),
                ProductAvailabilityId = table.Column<int>(),
                Published = table.Column<bool>(),
                Deleted = table.Column<bool>(),
                UpdateDate = table.Column<DateTime>(),
                CreateDate = table.Column<DateTime>()
            },
         constraints: table =>
         {
             table.PrimaryKey($"PK_{nameof(Product)}", x => x.Id);
             table.ForeignKey($"FK_{nameof(ProductAvailability)}", x => x.ProductAvailabilityId, nameof(ProductAvailability), "Id");
         });

            migrationBuilder.CreateTable(name: nameof(ShoppingCartItem), columns: table => new
            {
                Id = table.Column<int>().Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                CustomerId = table.Column<int>(nullable: true),
                CustomerCode = table.Column<string>(nullable: true),
                ProductId = table.Column<int>(),
                Quantity = table.Column<int>(),
                CurrentPriceInclTax = table.Column<decimal>(),
                Deleted = table.Column<bool>(),
                CreateDate = table.Column<DateTime>(),
                UpdateDate = table.Column<DateTime>(),
            },
          constraints: table =>
          {
              table.PrimaryKey($"PK_{nameof(ShoppingCartItem)}", x => x.Id);
              table.ForeignKey($"FK_{nameof(Product)}", x => x.ProductId, nameof(Product), "Id");
              table.ForeignKey($"FK_{nameof(Customer)}", x => x.CustomerId, nameof(Customer), "Id");
          });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: nameof(ShoppingCartItem));
            migrationBuilder.DropTable(name: nameof(Customer));
            migrationBuilder.DropTable(name: nameof(Product));
            migrationBuilder.DropTable(name: nameof(ProductAvailability));
        }
    }
}
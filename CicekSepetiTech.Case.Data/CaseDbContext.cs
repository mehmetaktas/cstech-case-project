using CicekSepetiTech.Case.Data.Configurations;
using CicekSepetiTech.Case.Data.DbEntity;
using Microsoft.EntityFrameworkCore;
using System;

namespace CicekSepetiTech.Case.Data
{
    public class CaseDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ProductAvailability> ProductAvailabilities { get; set; }
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }

        public CaseDbContext(DbContextOptions<CaseDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new ProductAvailabilityConfiguration());
            builder.ApplyConfiguration(new ShoppingCartItemConfiguration());

            builder.Entity<Customer>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<Product>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<ProductAvailability>().HasQueryFilter(x => !x.Deleted);
            builder.Entity<ShoppingCartItem>().HasQueryFilter(x => !x.Deleted);

            AddSeedData(builder);

            base.OnModelCreating(builder);
        }

        private void AddSeedData(ModelBuilder builder)
        {
            builder.Entity<Customer>().HasData(
                new Customer() { Id = 1, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, Email = "mehmet.aktas@outlook.com.tr", FirstName = "Mehmet", LastName = "Aktaş" },
                new Customer() { Id = 2, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, Email = "tech@ciceksepeti.com", FirstName = "Çiçek", LastName = "Sepeti" }
                );

            builder.Entity<ProductAvailability>().HasData(
                new ProductAvailability() { Id = 1, CreateDate = DateTime.Now, Name = "Satışta", Display = true, Salable = true },
                new ProductAvailability() { Id = 2, CreateDate = DateTime.Now, Name = "Temin Edilemiyor", Display = true, Salable = false },
                new ProductAvailability() { Id = 3, CreateDate = DateTime.Now, Name = "İnaktif", Display = false, Salable = false }
                );

            builder.Entity<Product>().HasData(
                new Product() { Id = 1, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, Code = "2054", Name = "Göz Model 925 Ayar Gümüş Kolye", PriceInclTax = 19.90m, Published = true, ProductAvailabilityId = 1, SalableQuantity = 20 },
                new Product() { Id = 2, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, Code = "3072936", Name = "Siyah Zincir Askılı Baget Çanta", PriceInclTax = 44.99m, Published = true, ProductAvailabilityId = 1, SalableQuantity = 0 },
                new Product() { Id = 3, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, Code = "1831101", Name = "Zincir ve Mini Cüzdan Detaylı Omuz Çantası", PriceInclTax = 39.99m, Published = true, ProductAvailabilityId = 2, SalableQuantity = 20 },
                new Product() { Id = 4, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, Code = "4370124", Name = "Gümüş Kaplama Papatya Figürlü Şans Bileklik", PriceInclTax = 14.99m, Published = true, ProductAvailabilityId = 3, SalableQuantity = 88 },
                new Product() { Id = 5, CreateDate = DateTime.Now, UpdateDate = DateTime.Now, Code = "6274578", Name = "14k Rose Altın Kaplama Pembe Zirkon Taşlı Lotus Çiçeği Kadın Kolye", PriceInclTax = 14.90m, Published = false, ProductAvailabilityId = 1, SalableQuantity = 99 }
                );
        }
    }
}
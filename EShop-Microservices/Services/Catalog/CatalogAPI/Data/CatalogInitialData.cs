using Marten;
using Marten.Schema;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CatalogAPI.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();

            if (await session.Query<Product>().AnyAsync())
                return;

            session.Store<Product>(GetPreconfiguredProducts());
            await session.SaveChangesAsync();
        }

        private static IEnumerable<Product> GetPreconfiguredProducts() => new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "iPhone 14 Pro",
                Description = "Apple's latest flagship smartphone with A16 Bionic chip.",
                ImageFile = "iphone14pro.png",
                Category = new List<string> { "smartphone", "electronics" }
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Samsung Galaxy S23",
                Description = "High-performance Android phone with a stunning display.",
                ImageFile = "galaxys23.png",
                Category = new List<string> { "smartphone", "electronics" }
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Google Pixel 8",
                Description = "Google's latest smartphone with pure Android experience.",
                ImageFile = "pixel8.png",
                Category = new List<string> { "smartphone", "android" }
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "OnePlus 11",
                Description = "Flagship killer with top-tier specs at a lower price.",
                ImageFile = "oneplus11.png",
                Category = new List<string> { "smartphone", "electronics" }
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Sony Xperia 5 V",
                Description = "Compact smartphone with excellent camera capabilities.",
                ImageFile = "xperia5v.png",
                Category = new List<string> { "smartphone", "camera", "android" }
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Motorola Edge 40",
                Description = "A sleek Android phone with solid performance.",
                ImageFile = "edge40.png",
                Category = new List<string> { "smartphone", "midrange" }
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Asus ROG Phone 7",
                Description = "Gaming smartphone with extreme performance and cooling.",
                ImageFile = "rogphone7.png",
                Category = new List<string> { "smartphone", "gaming" }
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Xiaomi 13 Pro",
                Description = "Feature-packed smartphone with Leica camera system.",
                ImageFile = "xiaomi13pro.png",
                Category = new List<string> { "smartphone", "photography" }
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Huawei P60 Pro",
                Description = "Premium phone with innovative camera tech and design.",
                ImageFile = "p60pro.png",
                Category = new List<string> { "smartphone", "camera", "photography" }
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Nothing Phone (2)",
                Description = "Unique design and user interface in a mid-range phone.",
                ImageFile = "nothingphone2.png",
                Category = new List<string> { "smartphone", "design" }
            }
        };
    }
}
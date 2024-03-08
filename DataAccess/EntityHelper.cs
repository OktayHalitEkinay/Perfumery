using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public static class EntityHelper
    {
        public static void SeedBrands(ModelBuilder modelBuilder)
        {
            List<string> perfumeNames = new() { "Hugo Boss", "Sephora", "Yves Rocher", "Nivea", "Rebull" };

            List<Brand> brands = new();
            int id = 1;
            foreach (string name in perfumeNames)
            {
                Brand brand = new Brand
                {
                    BrandId = id++,
                    BrandName = name,
                    Description = $"{name} Perfume"
                };
                brands.Add(brand);
            }

            modelBuilder.Entity<Brand>().HasData(brands);
        }
        public static void SeedPerfumes(ModelBuilder modelBuilder)
        {
            List<string> perfumeNames = new() { "White", "Yellow", "Blue", "Black" };
            List<decimal> perfumePrices = new() { 10, 20, 30 };
            List<int> brandIds = new() { 1, 2, 3, 4, 5 };

            List<Perfume> perfumes = new();
            Random random = new();
            int id = 1;
            HashSet<(string, int)> uniquePerfumeNames = new(); // Benzersiz parfüm adı ve marka kimliği çiftlerini tutmak için kullanılacak

            foreach (int brandId in brandIds)
            {
                // Her bir marka için döngü
                for (int i = 0; i < 3; i++) // Her bir marka için 3 parfüm oluştur
                {
                    string name;
                    do
                    {
                        name = perfumeNames[random.Next(perfumeNames.Count)]; // Rastgele bir parfüm adı seç
                    }
                    while (!uniquePerfumeNames.Add((name, brandId))); // Parfüm adı ve marka kimliği çiftini kontrol et, benzersizse ekle, değilse tekrar seç

                    decimal price = perfumePrices[random.Next(perfumePrices.Count)];

                    Perfume perfume = new()
                    {
                        PerfumeId = id++,
                        PerfumeName = name,
                        PhotoPath = "default",
                        Price = price,
                        BrandId = brandId
                    };
                    perfumes.Add(perfume);
                }
            }

            modelBuilder.Entity<Perfume>().HasData(perfumes);
        }
        public static void SeedUserDetils(ModelBuilder modelBuilder)
        {
            UserDetail userDetail = new()
            {
                UserName="oktayhalitekinay",
                FirstName="Oktay",
                LastName="Ekinay",
                Address="Bilecik",
                Email="oktayhalitekinay@gmail.com",
                Phone="05432719312",
                UserDetailId=1
            };
            modelBuilder.Entity<UserDetail>().HasData(userDetail);
        }

    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalkDbContext : DbContext
    {
        // this is used when inject the DbContext in Program.cs with own options
        public NZWalkDbContext(DbContextOptions<NZWalkDbContext> dbContextOptions):base(dbContextOptions) 
        {
                
        }
        
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed data for Difficulties
            // Easy , Medium , Hard
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("ebb9ae4c-86be-4f44-bf11-a1745df112bc") ,
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("0abb3597-85b5-4d9b-aeb1-abe20834765b") ,
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("8a8f1daf-aefc-492b-9463-2bfb5913dfb1") ,
                    Name = "Hard"
                },
            };
            //seed dificulties to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //seed data for Regions
            var regions = new List<Region>()
            {
                new Region() 
                {
                    Id = Guid.Parse("9c38ec4d-b715-495a-bd1c-6b050a85d25c"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = null
                },
                new Region()
                {
                    Id = Guid.Parse("de88803c-44a4-4c8c-bb5b-48d712547a33"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = null
                },
                new Region()
                {
                    Id = Guid.Parse("bea23893-d31a-4a99-adcc-f51f5ed5de7b"),
                    Name = "Bay Of Plenty",
                    Code = "BOP",
                    RegionImageUrl = null
                },
                new Region() 
                {
                    Id = Guid.Parse("01276bef-5613-43b8-baf4-2b24597fc7f7"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = null
                },
                 new Region()
                {
                    Id = Guid.Parse("f525993b-eec6-47b8-80c8-52923b3d2a27"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = null
                },
                  new Region()
                {
                    Id = Guid.Parse("4dde8174-be78-45ee-a15f-f2c6946f6409"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = null
                },
            };
            modelBuilder.Entity<Region>().HasData(regions);

        }
    }
}

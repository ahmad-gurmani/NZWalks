using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        //seed data for database
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            //seed data for difficulties
            // Easy, Medium, Hard
            var difficulties = new List<Difficulty>()
            {
                new()
                {
                    Id = Guid.Parse("43b73e5e-fce6-4bf7-82cf-0c6acfc28838"),
                    Name = "Easy"
                },  
                new()
                {
                    Id = Guid.Parse("d37febbf-1020-4d79-8f5f-7d717b6422bf"),
                    Name = "Medium"
                },  
                new()
                {
                    Id = Guid.Parse("83a89620-958d-4f66-a946-c9bd5ec2e5bf"),
                    Name = "Hard"
                },
            };

            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //seed data for regions

            var regions = new List<Region>()
            {
                new()
                {
                    Id = Guid.Parse("0147b3cd-e91f-424c-8b6f-57621605a926"),
                    Name = "AuckLand",
                    Code = "AKL",
                    RegionImageUrl = "https://ahdsdmad.com"
                },
                new()
                {
                    Id = Guid.Parse("2618cffb-7c72-4930-822c-9e216a137cae"),
                    Name = "Multan",
                    Code = "MN",
                    RegionImageUrl = "https://ahdsdmad.com"
                },
                new()
                {
                    Id = Guid.Parse("a41c6b3e-c709-44dc-9bb5-55350538c3ef"),
                    Name = "Walington",
                    Code = "WLT",
                    RegionImageUrl = "https://ahdsdmad.com"
                },
            };

            modelBuilder.Entity<Region>().HasData(regions);

        }
    }
}

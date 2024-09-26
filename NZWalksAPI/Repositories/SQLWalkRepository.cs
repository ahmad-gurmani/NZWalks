using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    //in this class we are doing concerete implementation of the methiods which are defined in the IWalkRepository interface
    public class SQLWalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext dbContext;
        public SQLWalkRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Walk> CreateAsync(Walk walk)
        {
            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<List<Walk>> GetAllAsync(string? filterOn = null, string? filterQuery = null)
        {
            var walks = dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

            // Filtering 
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walks = walks.Where(x => x.Name.Contains(filterQuery));
                }
            }

            return await walks.ToListAsync();
            //return await dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
            //or
            //return await dbContext.Walks.Include(x => x.Difficulty).Include(x => x.Region).ToListAsync();

        }

        public async Task<Walk?> GetByIdAsync(Guid id)
        {
            return await dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walk?> UpdateAsync(Guid id, Walk walk)
        {
            var existingWalk = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalk == null)
            {
                return null;
            }

            //If existingWalk is not null then Update the existing walk
            existingWalk.Name = walk.Name;
            existingWalk.Description = walk.Description;
            existingWalk.LengthInKm = walk.LengthInKm;
            existingWalk.WalkImageUrl = walk.WalkImageUrl;
            existingWalk.DifficultyId = walk.DifficultyId;
            existingWalk.RegionId = walk.RegionId;

            await dbContext.SaveChangesAsync();

            return existingWalk;

        }

        public async Task<Walk?> DeleteAsync(Guid id)
        {
            var existingWalkDomainModel = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (existingWalkDomainModel == null)
            {
                return null;
            }

            dbContext.Walks.Remove(existingWalkDomainModel);

            await dbContext.SaveChangesAsync();

            return existingWalkDomainModel;
        }
    }
}

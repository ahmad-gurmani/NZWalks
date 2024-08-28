using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        //method defination
        Task<List<Region>> GetAllAsync();

        Task<Region?> GetByIdAsync(Guid id);

        Task<Region> CreateAsync(Region region);
        Task<Region?> UpdateAsync(Guid id,Region region);
        Task<Region?> DeleteAsync(Guid id);
    }
}

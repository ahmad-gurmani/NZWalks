using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.DTO
{
    public class AddWalkRequestDto
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }
    }
}

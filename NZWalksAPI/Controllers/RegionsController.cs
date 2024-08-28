using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly NZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;

        public RegionsController(NZWalksDbContext dbContext, IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }

        //GET: ALL REGIONS
        //GET: https://localhost:portnumber/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var regionsDomain = await regionRepository.GetAllAsync();

            // map domain models to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageUrl = regionDomain.RegionImageUrl
                });
            }

            return Ok(regionsDto);
        }

        //GET: SINGLE REGIONS
        //GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //find only take PK not other values but FirstOrDefault take any field eg.Code, Name, ImageUrl etc

            //Get region domain model from database
            var regionDomain = await regionRepository.GetByIdAsync(id);

            if (regionDomain == null)
            {
                return NotFound();
            }

            //Map/convert domain models to DTOs
            var regionsDto = new RegionDto
            {
                Id = regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl
            };


            return Ok(regionsDto);
        }

        //Post: CREATE NEW REGIONS
        //Post: https://localhost:portnumber/api/regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //map or convert DTO to domain model
            var regionDomainModel = new Region
            {
                Name = addRegionRequestDto.Name,
                Code = addRegionRequestDto.Code,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };

            // Use Domain Model to create Region  
            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            //map or convert domain model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Name = regionDomainModel.Name,
                Code = regionDomainModel.Code,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        //PUT: Update region
        //PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Map or convert Dto to Domain model
            var regionDomainModel = new Region
            {
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl,
            };

            regionDomainModel = await regionRepository.UpdateAsync(id, regionDomainModel);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //convert Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return Ok(regionDto);
        }

        //Delete region
        //DELETE: https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var regionDomainModel = await regionRepository.DeleteAsync(id);

            if (regionDomainModel == null)
            {
                return NotFound();
            }

            //return the deleted region  back
            //map domain model to DTOs
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return Ok(regionDto);
        }

    }
}
 
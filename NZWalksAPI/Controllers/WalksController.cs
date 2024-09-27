using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IWalkRepository walkRepository;

        public WalksController(IMapper mapper, IWalkRepository walkRepository)
        {
            this.mapper = mapper;
            this.walkRepository = walkRepository;
        }

        //CREATE WALKS
        //POST: /api/walks
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] AddWalkRequestDto addWalkRequestDto)
        {
            if (addWalkRequestDto == null)
            {
                return NotFound();
            }

            //Map AddWalkRequestDto to Walk domain model
            var walkDomainModel = mapper.Map<Walk>(addWalkRequestDto);

            await walkRepository.CreateAsync(walkDomainModel);

            //map domain model to Dto
            var walkData = mapper.Map<WalkDto>(walkDomainModel);

            return Ok(walkData);
        }

        // GET ALL WALKS
        //GET: /api/walks
        //GET: /api/walks?filterOn=Name&filterQurey=Track
        //GET: /api/walks?filterOn=Name&filterQurey=Track&sortBy=Name&isAscending=true
        //GET: /api/walks?filterOn=Name&filterQurey=Track&sortBy=Name&isAscending=true&pageNumber=1&pageSize=10
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1000)
        {
            var walksDomainModel = await walkRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true,
                pageNumber, pageSize);

            //Map domain model to Dto
            var walksDto = mapper.Map<List<WalkDto>>(walksDomainModel);

            return Ok(walksDto);
        }

        // GET SINGLE WALK by id
        //GET: /api/walks/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {

            var walkDomainModel = await walkRepository.GetByIdAsync(id);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            // Map/convert walk domain model to walk dto   
            var walkDto = mapper.Map<WalkDto>(walkDomainModel);

            return Ok(walkDto);
        }

        //PUT: Update walk by id
        //PUT: https://localhost:portnumber/api/Walks/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateWalkRequestDto updateWalkRequestDto)
        {
            //Map Dto to domain Model
            var walkDomainModel = mapper.Map<Walk>(updateWalkRequestDto);

            walkDomainModel = await walkRepository.UpdateAsync(id, walkDomainModel);

            if (walkDomainModel == null)
            {
                return NotFound();
            }

            var walkDto = mapper.Map<WalkDto>(walkDomainModel);

            return Ok(walkDto);
        }

        //DELETE: DELETE walk by id
        //DELETE: https://localhost:portnumber/api/Walks/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var walkdomainModel = await walkRepository.DeleteAsync(id);

            if (walkdomainModel == null)
            {
                return NotFound();
            }

            var walkDto = mapper.Map<WalkDto>(walkdomainModel);

            return Ok(walkDto);
        }
    }
}

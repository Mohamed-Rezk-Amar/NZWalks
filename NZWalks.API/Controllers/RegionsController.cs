using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;
using NZWalks.API.Repository.IRepository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class RegionsController : ControllerBase
    {
    
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public RegionsController(IUnitOfWork unitOfWork,IMapper mapper)
        {
          
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }


        // GET ALL REGIONS
        // GET: https://localhost:portnumber/api/regions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // Get Region Domain models from database  
            var regionsDomain = await unitOfWork.Region.GetAllAsync();
            #region Old Mapping
            /*
            // Map Regions Domain Models to DTOs
            var regionsDto = new List<RegionDto>();
            foreach (var regionDomain in regionsDomain) 
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Name = regionDomain.Name,
                    Code = regionDomain.Code,
                    RegionImageUrl = regionDomain.RegionImageUrl,
                });
            }
            */
            #endregion

            // Map Regions Domain Models to List of Regions DTOs and Return DTOs to client
            return Ok(mapper.Map<List<RegionDto>>(regionsDomain)); 

        }


        // GET SINGLE REGION 
        // GET: https://localhost:portnumber/api/regions/{id}
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            // Get Region Domain Model From Database
            var regionDomain = await unitOfWork.Region.GetByIdAsync(u =>u.Id == id);
            if (regionDomain == null)
            {
                return NotFound();
            }

            /*
            var regionDto = new RegionDto
            {
                Id=regionDomain.Id,
                Name = regionDomain.Name,
                Code = regionDomain.Code,
                RegionImageUrl = regionDomain.RegionImageUrl,
            };
            */
           ;
            // Map Region Domain Models to DTOs and Return DTO to client
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }


        // POST To Create New Region
        // POST: https://localhost:portnumber/api/regions
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] RegionDto regionDtoRequest)
        {
                // Map DTO to Domain Model
                var regionDomainModel = mapper.Map<Region>(regionDtoRequest);

                // Use Domain Model to Create/[Save] Region To Db
                regionDomainModel = await unitOfWork.Region.CreateAsync(regionDomainModel);

                // Map Domain Model to DTO To return to Client [Optional]
                var regionDto = mapper.Map<RegionDto>(regionDomainModel);

                return CreatedAtAction(nameof(GetById), new { id = regionDto.Id }, regionDto);
        }

        // Update Region
        //PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute]Guid id , [FromBody] RegionDto regionDtoRequest)
        {          
                //Map DTO to Domain Model
                var regionDomain = mapper.Map<Region>(regionDtoRequest);
                //Check if  region exists
                var regionDomainModel = await unitOfWork.Region.UpdateAsync(id, regionDomain);
                if (regionDomainModel == null)
                {
                    return NotFound();
                }
                // Map Domain Model to DTO and return DTO to client
                return Ok(mapper.Map<RegionDto>(regionDomainModel));
        }


        // Delete Region
        //DELETE: https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DelelteById([FromRoute] Guid id) 
        {
            var regionDomain = await unitOfWork.Region.DeleteAsync(id);
            if (regionDomain == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<RegionDto>(regionDomain));
        }

    }
}

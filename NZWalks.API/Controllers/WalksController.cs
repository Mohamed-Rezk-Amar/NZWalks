using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repository;
using NZWalks.API.Repository.IRepository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController  : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public WalksController(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> Create([FromBody] WalkDTOCreateandUpdate walkDTOFromRequest)
        {         
                // Map DTO to Domain Model
                var walkDomain = mapper.Map<Walk>(walkDTOFromRequest);
                walkDomain = await unitOfWork.Walk.CreateAsync(walkDomain);
                // Map Domain Model to DTO and return to client
                return Ok(mapper.Map<WalkDTOCreateandUpdate>(walkDomain));                                        
        }

        // GET Walks
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           var walksDomain = await unitOfWork.Walk.GetAllAsync(includeProperties:"Region,Difficulty");
            return Ok(mapper.Map<List<WalkDTO>>(walksDomain));

        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute]Guid id)
        {
            var walkDomain = await unitOfWork.Walk.GetByIdAsync(u => u.Id ==id);
            if (walkDomain == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDTO>(walkDomain));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute]Guid id)
        {
            var walkDomain = await unitOfWork.Walk.DeleteAsync(id);
            if (walkDomain == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<WalkDTO>(walkDomain));
        }

        [HttpPut]
        [Route("{id}")]
        [ValidateModel]
        public async Task<IActionResult> Update([FromRoute]Guid id, [FromBody] WalkDTOCreateandUpdate WalkDTOFromRequest)
        {          
                var walkDomain = mapper.Map<Walk>(WalkDTOFromRequest);
                walkDomain = await unitOfWork.Walk.UpdateAsync(id, walkDomain);
                if (walkDomain == null)
                {
                    return NotFound();
                }
                return Ok(mapper.Map<WalkDTOCreateandUpdate>(walkDomain));
        }
    }
}

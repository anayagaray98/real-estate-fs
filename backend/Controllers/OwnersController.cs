using Microsoft.AspNetCore.Mvc;
using RealEstateAPI.Dtos;
using RealEstateAPI.Services;
using RealEstateAPI.Models;

namespace RealEstateAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnersController : ControllerBase
    {
        private readonly OwnerService _ownerService;

        public OwnersController(OwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        // GET: api/owners
        [HttpGet]
        public async Task<ActionResult<List<OwnerDto>>> GetAll()
        {
            var owners = await _ownerService.GetAllAsync();
            return Ok(owners);
        }

        // GET: api/owners/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OwnerDto>> GetById(string id)
        {
            var owner = await _ownerService.GetByIdAsync(id);
            if (owner == null)
                return NotFound();

            return Ok(owner);
        }

        // POST: api/owners
        [HttpPost]
        public async Task<ActionResult<OwnerDto>> Create([FromBody] OwnerDto newOwner)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdOwner = await _ownerService.CreateAsync(newOwner);
            return CreatedAtAction(nameof(GetById), new { id = createdOwner.IdOwner }, createdOwner);
        }

        // PUT: api/owners/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<OwnerDto>> Update(string id, [FromBody] OwnerDto updatedOwner)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var owner = await _ownerService.UpdateAsync(id, updatedOwner);
            if (owner == null)
                return NotFound();

            return Ok(owner); // returns 200 + the updated object
        }

        // DELETE: api/owners/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var deleted = await _ownerService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}

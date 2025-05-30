using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AviationSystem.REST.Models;
using Microsoft.AspNetCore.Mvc;
using AviationSystem.REST.Services;

namespace YourApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MilitaryAircraftController : ControllerBase
    {
        private readonly ICrudServiceAsync<MilitaryAircraftModel> _service;

        public MilitaryAircraftController(ICrudServiceAsync<MilitaryAircraftModel> service)
        {
            _service = service;
        }

        // GET: api/MilitaryAircraft
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MilitaryAircraftModel>>> GetAll()
        {
            var items = await _service.ReadAllAsync();
            return Ok(items);
        }

        // GET: api/MilitaryAircraft/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<MilitaryAircraftModel>> GetById(Guid id)
        {
            var item = await _service.ReadAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // POST: api/MilitaryAircraft
        [HttpPost]
        public async Task<ActionResult> Create([FromBody] MilitaryAircraftModel model)
        {
            var created = await _service.CreateAsync(model);
            if (!created) return BadRequest();
            await _service.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        // PUT: api/MilitaryAircraft/{id}
        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] MilitaryAircraftModel model)
        {
            if (id != model.Id) return BadRequest();
            var exists = await _service.ReadAsync(id);
            if (exists == null) return NotFound();

            await _service.UpdateAsync(model);
            await _service.SaveAsync();
            return NoContent();
        }

        // DELETE: api/MilitaryAircraft/{id}
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var exists = await _service.ReadAsync(id);
            if (exists == null) return NotFound();

            await _service.RemoveAsync(exists);
            await _service.SaveAsync();
            return NoContent();
        }
    }
}
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
    public class CargoAircraftController : ControllerBase
    {
        private readonly ICrudServiceAsync<CargoAircraftModel> _service;

        public CargoAircraftController(ICrudServiceAsync<CargoAircraftModel> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CargoAircraftModel>>> GetAll()
            => Ok(await _service.ReadAllAsync());

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<CargoAircraftModel>> GetById(Guid id)
        {
            var item = await _service.ReadAsync(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CargoAircraftModel model)
        {
            if (!await _service.CreateAsync(model)) return BadRequest();
            await _service.SaveAsync();
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] CargoAircraftModel model)
        {
            if (id != model.Id) return BadRequest();
            if (await _service.ReadAsync(id) == null) return NotFound();

            await _service.UpdateAsync(model);
            await _service.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var item = await _service.ReadAsync(id);
            if (item == null) return NotFound();

            await _service.RemoveAsync(item);
            await _service.SaveAsync();
            return NoContent();
        }
    }
}
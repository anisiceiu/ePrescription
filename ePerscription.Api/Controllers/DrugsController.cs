using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ePerscription.Application.DTOs;
using ePerscription.Application.Interfaces;

namespace ePerscription.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrugsController : ControllerBase
    {
        private readonly IDrugService _service;

        public DrugsController(IDrugService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _service.GetAllDrugsAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var drug = await _service.GetDrugByIdAsync(id);
            if (drug == null) return NotFound();
            return Ok(drug);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DrugDto drugDto)
        {
            await _service.AddDrugAsync(drugDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DrugDto drugDto)
        {
            if (id != drugDto.Id) return BadRequest();
            await _service.UpdateDrugAsync(drugDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteDrugAsync(id);
            return NoContent();
        }
    }
}

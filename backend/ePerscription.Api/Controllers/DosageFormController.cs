using ePerscription.Application.DTOs;
using ePerscription.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ePerscription.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DosageFormController : ControllerBase
    {
        private readonly IDosageFormService _service;

        public DosageFormController(IDosageFormService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _service.GetAllDosageFormsAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var DosageForm = await _service.GetDosageFormByIdAsync(id);
            if (DosageForm == null) return NotFound();
            return Ok(DosageForm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DosageFormDto DosageFormDto)
        {
            await _service.AddDosageFormAsync(DosageFormDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, DosageFormDto DosageFormDto)
        {
            if (id != DosageFormDto.DosageFormId) return BadRequest();
            await _service.UpdateDosageFormAsync(DosageFormDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteDosageFormAsync(id);
            return NoContent();
        }
    }
}

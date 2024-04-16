using Microsoft.AspNetCore.Mvc;
using quizon.Models;
using quizon.Services;

namespace quizon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OptionController : ControllerBase
    {
        IOptionService optionService;
        public OptionController(IOptionService service)
        {
            optionService = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(optionService.Get());
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            var option = await optionService.GetId(id);
            return Ok(option);
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Option option)
        {
            var newOption = await optionService.Save(option);
            return Ok(newOption);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Option option)
        {
            var updated = await optionService.Update(id, option);
            return Ok(updated);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await optionService.Delete(id);
            return Ok();
        }
    }
}
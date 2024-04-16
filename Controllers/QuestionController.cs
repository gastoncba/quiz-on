using Microsoft.AspNetCore.Mvc;
using quizon.Dto;
using quizon.Models;
using quizon.Services;

namespace quizon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        IQuestionService questionService;
        public QuestionController(IQuestionService service)
        {
            questionService = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(questionService.Get());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            var question = await questionService.GetId(id);
            return Ok(question);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] QuestionCreateDTO question)
        {
            var newQuestion = await questionService.Save(question);
            return Ok(newQuestion);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Question question)
        {
            var updated = await questionService.Update(id, question);
            return Ok(updated);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await questionService.Delete(id);
            return Ok();
        }
    }
}
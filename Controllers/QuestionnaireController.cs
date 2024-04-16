using Microsoft.AspNetCore.Mvc;
using quizon.Services;

namespace quizon.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionnaireController : ControllerBase
    {
        IQuestionService questionService;
        public QuestionnaireController(IQuestionService service)
        {
            questionService = service;
        }

        [HttpGet]
        [Route("{categoryId}")]
        public async Task<IActionResult> GetQuestionnaire(int categoryId)
        {
            var questionnaire = await questionService.GetQuestionnaire(categoryId);
            return Ok(questionnaire);
        }
    }
}
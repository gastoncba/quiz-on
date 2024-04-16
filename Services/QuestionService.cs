using Microsoft.EntityFrameworkCore;
using System.Net;
using quizon.Dto;
using quizon.Exceptions;
using quizon.Models;
using quizon.Utils;

namespace quizon.Services
{
    public class QuestionService : IQuestionService
    {
        QuizOnContext context;

        public QuestionService(QuizOnContext dbContext)
        {
            context = dbContext;
        }

        public IEnumerable<QuestionDTO> Get()
        {
            IEnumerable<Question> questions = context.questions
                .Include(q => q.category)
                .Include(q => q.options);

            IEnumerable<QuestionDTO> questionDtos = questions.Select(q => new QuestionDTO(q));


            return questionDtos;
        }

        public async Task<QuestionDTO?> GetId(int id)
        {
            var foundQuestion = await context.questions
            .Include(q => q.category)
            .Include(q => q.options)
            .FirstOrDefaultAsync(q => q.id == id) ?? throw new HttpResponseException(HttpStatusCode.NotFound, "Pregunta no econtrada");

            var question = new QuestionDTO(foundQuestion);
            return question;
        }

        public async Task<QuestionDTO?> Save(QuestionCreateDTO question)
        {
            //creamos la pregunta 
            Question newQuestion = new()
            {
                categoryId = question.categoryId,
                title = question.title
            };

            await context.AddAsync(newQuestion);
            await context.SaveChangesAsync();

            //creamos las opciones 
            foreach (var opt in question.options)
            {
                Option newOption = new()
                {
                    value = opt.value,
                    isCorrect = opt.isCorrect,
                    questionId = newQuestion.id
                };
                await context.AddAsync(newOption);
                await context.SaveChangesAsync();
            }

            var completeQuestion = await GetId(newQuestion.id);
            return completeQuestion;
        }

        public async Task<Question> Update(int id, Question question)
        {
            var currentQuestion = await context.questions.FindAsync(id) ?? throw new HttpResponseException(HttpStatusCode.NotFound);
            currentQuestion.categoryId = question.categoryId;
            currentQuestion.title = question.title;
            await context.SaveChangesAsync();
            return currentQuestion;
        }

        public async Task Delete(int id)
        {
            //encontramos la pregunta
            var foundQuestion = await context.questions.FindAsync(id) ?? throw new HttpResponseException(HttpStatusCode.NotFound);

            //encontramos las opciones de la pregunta y las eliminamos
            var options = await context.options.Where(o => o.questionId == id).ToArrayAsync();
            foreach (var option in options)
            {
                context.Remove(option);
                await context.SaveChangesAsync();
            }

            //Eliminamos la pregunta
            context.Remove(foundQuestion);
            await context.SaveChangesAsync();
        }

        public async Task<List<QuestionDTO>> GetQuestionnaire(int categoryId)
        {
            List<Question> questions = await context.questions
            .Include(q => q.category)
            .Include(q => q.options)
            .Where(q => q.categoryId == categoryId).ToListAsync();

            if (questions.Count == 0) throw new HttpResponseException(HttpStatusCode.NotFound, "No se encontraron preguntas de esa categoria");

            Random rnd = new();
            int seed = rnd.Next(123);
            int limit = questions.Count;
            int count = (questions.Count > 10) ? 10 : questions.Count;
            int[] numbers = RandomUtils.GenerateRandomNumbers(seed, limit, count);
            List<QuestionDTO> questionDtos = new();

            foreach (int num in numbers)
            {
                Question question = questions[num];
                questionDtos.Add(new QuestionDTO(question));
            }

            return questionDtos;
        }
    }

    public interface IQuestionService
    {
        IEnumerable<QuestionDTO> Get();
        Task<QuestionDTO?> GetId(int id);
        Task<QuestionDTO?> Save(QuestionCreateDTO question);
        Task<Question> Update(int id, Question question);
        Task Delete(int id);
        Task<List<QuestionDTO>> GetQuestionnaire(int categoryId);
    }
}
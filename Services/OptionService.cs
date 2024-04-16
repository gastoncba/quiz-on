using System.Net;
using quizon.Exceptions;
using quizon.Models;

namespace quizon.Services
{
    public class OptionService : IOptionService
    {
        QuizOnContext context;

        public OptionService(QuizOnContext dbContext)
        {
            context = dbContext;
        }

        public IEnumerable<Option> Get()
        {
            return context.options; ;
        }

        public async Task<Option?> GetId(int id)
        {
            return await context.options.FindAsync(id) ?? throw new HttpResponseException(HttpStatusCode.NotFound);
        }

        public async Task<Option> Save(Option option)
        {
            await context.AddAsync(option);
            await context.SaveChangesAsync();
            return option;
        }

        public async Task<Option> Update(int id, Option option)
        {
            var currentOption = await context.options.FindAsync(id) ?? throw new HttpResponseException(HttpStatusCode.NotFound);
            currentOption.isCorrect = option.isCorrect;
            currentOption.value = option.value;
            currentOption.questionId = option.questionId;
            await context.SaveChangesAsync();
            return option;
        }

        public async Task Delete(int id)
        {
            var foundOption = await context.options.FindAsync(id) ?? throw new HttpResponseException(HttpStatusCode.NotFound);
            context.Remove(foundOption);
            await context.SaveChangesAsync();
        }
    }

    public interface IOptionService
    {
        IEnumerable<Option> Get();
        Task<Option?> GetId(int id);
        Task<Option> Save(Option option);
        Task<Option> Update(int id, Option option);
        Task Delete(int id);
    }
}
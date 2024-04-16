using System.Net;
using quizon.Exceptions;
using quizon.Models;

namespace quizon.Services
{
    public class CategoryService : ICategoryService
    {
        QuizOnContext context;

        public CategoryService(QuizOnContext dbContext)
        {
            context = dbContext;
        }

        public IEnumerable<Category> Get()
        {
            return context.categories;
        }
        public async Task<Category?> GetId(int id)
        {
            return await context.categories.FindAsync(id) ?? throw new HttpResponseException(HttpStatusCode.NotFound);
        }
        public async Task<Category> Save(Category category)
        {
            await context.AddAsync(category);
            await context.SaveChangesAsync();
            return category;
        }
        public async Task<Category> Update(int id, Category category)
        {
            var currentCategory = await context.categories.FindAsync(id) ?? throw new HttpResponseException(HttpStatusCode.NotFound);
            currentCategory.name = category.name;
            await context.SaveChangesAsync();
            return category;
        }

        public async Task Delete(int id)
        {
            var currentCategory = await context.categories.FindAsync(id) ?? throw new HttpResponseException(HttpStatusCode.NotFound);
            context.Remove(currentCategory);
            await context.SaveChangesAsync();
        }

    }

    public interface ICategoryService
    {
        IEnumerable<Category> Get();
        Task<Category?> GetId(int id);
        Task<Category> Save(Category category);
        Task<Category> Update(int id, Category category);
        Task Delete(int id);
    }
}
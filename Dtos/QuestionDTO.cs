using quizon.Models;

namespace quizon.Dto
{
    public class QuestionDTO
    {
        public int id { get; set; }
        public string title { get; set; }
        public Category category { get; set; }
        public ICollection<OptionDTO> options { get; set; }
        public QuestionDTO(Question question)
        {
            id = question.id;
            title = question.title;
            category = question.category;
            options = question.options.Select(option => new OptionDTO
            {
                id = option.id,
                value = option.value,
                isCorrect = option.isCorrect
            }).ToList();
        }
    }

    public class QuestionCreateDTO
    {
        public string title { get; set; }
        public int categoryId { get; set; }
        public ICollection<OptionCreateDTO> options { get; set; }
    }
}
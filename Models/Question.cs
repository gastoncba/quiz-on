using System.Text.Json.Serialization;

namespace quizon.Models
{
    public class Question
    {
        public int id { get; set; }
        public int categoryId { get; set; }
        public string title { get; set; }
        [JsonIgnore]
        public virtual Category? category { get; set; }
        [JsonIgnore]
        public virtual ICollection<Option>? options { get; set; }
    }
}
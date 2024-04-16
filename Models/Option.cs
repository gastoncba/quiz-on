using System.Text.Json.Serialization;

namespace quizon.Models
{
    public class Option
    {
        public int id { get; set; }
        public int questionId { get; set; }
        public string value { get; set; }
        public bool isCorrect { get; set; }
        [JsonIgnore]
        public virtual Question? question { get; set; }
    }
}
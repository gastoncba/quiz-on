using System.Text.Json.Serialization;

namespace quizon.Models
{
    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Question>? questions { get; set; }
    }
}
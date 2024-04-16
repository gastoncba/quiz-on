namespace quizon.Dto
{
    public class OptionDTO
    {
        public int id { get; set; }
        public string value { get; set; }
        public bool isCorrect { get; set; }
    }

    public class OptionCreateDTO
    {
        public string value { get; set; }
        public bool isCorrect { get; set; }
    }
}
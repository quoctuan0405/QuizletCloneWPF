namespace QuizletClone.API.Payload
{
    public class TermPayload
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public string? Explanation { get; set; }
    }
}

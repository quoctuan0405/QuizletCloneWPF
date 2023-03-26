namespace QuizletClone.API.Payload
{
    public class SetPayload
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<TermPayload> Terms { get; set; }
    }
}

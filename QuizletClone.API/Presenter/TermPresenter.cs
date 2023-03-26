namespace QuizletClone.API.Presenter
{
    public class TermPresenter
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public string Explanation { get; set; }

        public List<string> Choices { get; set; }
    }
}

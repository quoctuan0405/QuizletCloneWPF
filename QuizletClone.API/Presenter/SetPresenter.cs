namespace QuizletClone.API.Presenter
{
    public class SetPresenter
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TermCount { get; set; }

        public UserPresenter Author { get; set; }
    }
}

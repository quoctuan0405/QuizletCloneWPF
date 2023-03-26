namespace QuizletClone.API.Presenter
{
    public class SetDetailPresenter
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public UserPresenter Author { get; set; }

        public List<TermPresenter> Terms { get; set; }
    }
}

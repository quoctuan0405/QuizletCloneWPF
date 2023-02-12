using System.ComponentModel.DataAnnotations.Schema;

namespace QuizletClone.Domain.Model
{
    public class Set : DomainObject
    {
        public string Name { get; set; }

        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public User Author { get; set; }

        public IEnumerable<Term> Terms { get; set; }
    }
}
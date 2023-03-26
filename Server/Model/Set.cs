using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Model
{
    public class Set : DomainObject
    {
        public string Name { get; set; }

        [ForeignKey("Author")]
        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }

        public IEnumerable<Term> Terms { get; set; }
    }
}
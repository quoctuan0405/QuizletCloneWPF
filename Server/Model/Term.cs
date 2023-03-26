using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    public class Term : DomainObject
    {
        public string Question { get; set; }

        public string Answer { get; set; }

        public string? Explanation { get; set; }

        [ForeignKey("Set")]
        public int SetId { get; set; }
        public Set Set { get; set; }

        [NotMapped]
        public virtual List<string> Choices { get; set; }
    }
}

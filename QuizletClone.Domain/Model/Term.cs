using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizletClone.Domain.Model
{
    public class Term
    {
        public int Id { get; set; }
        string Question { get; set; }

        string Answer { get; set; }

        string Explanation { get; set; }

        [ForeignKey("Set")]
        public int SetId { get; set; }
        public Set Set { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    public class UserLearningTerm : DomainObject
    {
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("Term")]
        public int TermId { get; set; }
        public Term Term { get; set; }

        public int Remained { get; set; }

        public bool Learned { get; set; }
    }
}

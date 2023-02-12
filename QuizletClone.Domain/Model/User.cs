using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizletClone.Domain.Model
{
    public class User : DomainObject
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public IEnumerable<Set> Sets { get; set; }
    }
}

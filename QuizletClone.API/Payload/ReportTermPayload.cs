using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizletClone.API.Payload
{
    public class ReportTermPayload
    {
        public int TermId { get; set; }

        public bool Correct { get; set; }
    }
}

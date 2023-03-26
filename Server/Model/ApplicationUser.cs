using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Model
{
    public class ApplicationUser : IdentityUser
    {
        public IEnumerable<Set> Sets { get; set; }
    }
}

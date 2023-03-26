using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace QuizletClone.WPF.Security
{
    public interface IHavePassword
    {
        public SecureString SecurePassword { get; }
    }
}

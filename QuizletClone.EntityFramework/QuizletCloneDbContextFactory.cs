using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizletClone.EntityFramework
{
    public class QuizletCloneDbContextFactory : IDesignTimeDbContextFactory<QuizletCloneDbContext>
    {
        public QuizletCloneDbContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<QuizletCloneDbContext>();

            options.UseSqlServer("Server=localhost;Database=quizlet_clone;Trusted_Connection=True;Trust Server Certificate=True");

            return new QuizletCloneDbContext(options.Options);
        }
    }
}

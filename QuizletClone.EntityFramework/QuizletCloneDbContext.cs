using Microsoft.EntityFrameworkCore;
using QuizletClone.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizletClone.EntityFramework
{
    public class QuizletCloneDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Term> Terms { get; set; }

        public DbSet<Set> Sets { get; set; }

        public DbSet<UserLearningTerm> LearningTerms { get; set; }

        public QuizletCloneDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLearningTerm>().HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserLearningTerm>().HasOne(e => e.Term).WithMany().HasForeignKey(e => e.TermId).OnDelete(DeleteBehavior.NoAction);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Term> Terms { get; set; }

        public DbSet<Set> Sets { get; set; }

        public DbSet<UserLearningTerm> LearningTerms { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLearningTerm>().HasOne(e => e.User).WithMany().HasForeignKey(e => e.UserId).OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserLearningTerm>().HasOne(e => e.Term).WithMany().HasForeignKey(e => e.TermId).OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);

            // Bỏ tiền tố AspNet của các bảng: mặc định
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

            // Seed data
            modelBuilder.Entity<Set>().HasData(
                new Set()
                {
                    Id = 1,
                    AuthorId = "77a8e0c0-841d-4ccf-b930-658cac62b95c",
                    Name = "Set 1"
                },
                new Set(){
                    Id = 2,
                    AuthorId = "77a8e0c0-841d-4ccf-b930-658cac62b95c",
                    Name = "Set 2"
                },
                new Set()
                {
                    Id = 3,
                    AuthorId = "77a8e0c0-841d-4ccf-b930-658cac62b95c",
                    Name = "Set 3"
                },
                new Set()
                {
                    Id = 4,
                    AuthorId = "77a8e0c0-841d-4ccf-b930-658cac62b95c",
                    Name = "Set 4"
                }
            );

            modelBuilder.Entity<Term>().HasData(
                new Term()
                {
                    Id = 1,
                    SetId = 1,
                    Question = "question 1",
                    Answer = "answer 1",
                    Explanation = "explanation 1"
                },
                new Term()
                {
                    Id = 2,
                    SetId = 1,
                    Question = "question 2",
                    Answer = "answer 2",
                    Explanation = "explanation 2"
                },
                new Term()
                {
                    Id = 3,
                    SetId = 1,
                    Question = "question 3",
                    Answer = "answer 3",
                    Explanation = "explanation 1"
                },
                new Term()
                {
                    Id = 4,
                    SetId = 1,
                    Question = "question 4",
                    Answer = "answer 4",
                    Explanation = "explanation 4"
                },
                new Term()
                {
                    Id = 5,
                    SetId = 2,
                    Question = "question 1",
                    Answer = "answer 1",
                    Explanation = "explanation 1"
                }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}

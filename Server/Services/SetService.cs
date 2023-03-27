using Microsoft.EntityFrameworkCore;
using Server.Model;
using Server.Services.Base;
using Server.Data;
using QuizletClone.API.Payload;

namespace Server.Services
{
    public class SetService : GenericDataService<Set>
    {
        public SetService(IDbContextFactory<ApplicationDbContext> applicationDbContextFactory) : base(applicationDbContextFactory)
        {
        }

        public async Task<IEnumerable<Set>> GetList(string keyword)
        {
            using (ApplicationDbContext context = _quizletCloneDbContextFactory.CreateDbContext())
            {
                IQueryable<Set> query = context.Set<Set>()
                    .Include(s => s.Author)
                    .Include(s => s.Terms);

                if (keyword != null)
                {
                    query = query.Where(s => s.Name.Contains(keyword));
                }

                IEnumerable<Set> sets = query.ToList();

                return sets;
            }
        }

        public async Task<IEnumerable<Set>> GetMySets(string userId, string keyword)
        {
            using (ApplicationDbContext context = _quizletCloneDbContextFactory.CreateDbContext())
            {
                IQueryable<Set> query = context.Set<Set>()
                    .Where(s => s.AuthorId.Equals(userId))
                    .Include(s => s.Author)
                    .Include(s => s.Terms);

                if (keyword != null)
                {
                    query = query.Where(s => s.Name.Contains(keyword));
                }

                IEnumerable<Set> sets = query.ToList();

                return sets;
            }
        }

        public async Task<Set> Get(int id)
        {
            using (ApplicationDbContext context = _quizletCloneDbContextFactory.CreateDbContext())
            {
                Set? entity = context.Set<Set>()
                    .Include(s => s.Author)
                    .Include(s => s.Terms)
                    .FirstOrDefault((e) => e.Id == id);

                return entity;
            }
        }

        public async Task<Set> Update(int id, Set set)
        {
            using (ApplicationDbContext context = _quizletCloneDbContextFactory.CreateDbContext())
            {
                List<Term> updatedTerms = new List<Term>();
                List<Term> createdTerms = new List<Term>();

                foreach(var term in set.Terms)
                {
                    term.SetId = set.Id;

                    if (term.Id == 0)
                    {
                        createdTerms.Add(term);
                    } 
                    else
                    {
                        updatedTerms.Add(term);
                    }
                }

                // Update terms that user submit
                context.Terms.UpdateRange(updatedTerms);

                await context.SaveChangesAsync();

                // Remove all terms from the set that user not submitted
                List<int> updatedTermIds = new List<int>();

                foreach(var term in updatedTerms)
                {
                    updatedTermIds.Add(term.Id);
                }

                var terms = await context.Terms
                        .Where(t => t.SetId == id)
                        .Where(t => !updatedTermIds.Contains(t.Id))
                        .ToListAsync();

                List<int> removeTermIds = new List<int>();

                foreach (var term in terms)
                {
                    removeTermIds.Add(term.Id);
                }

                var userLearningTerms = await context.LearningTerms
                        .Where(lt => removeTermIds.Contains(lt.TermId))
                        .ToListAsync();

                foreach(var userLearningTerm in userLearningTerms)
                {
                    context.LearningTerms.Remove(userLearningTerm);
                }
                await context.SaveChangesAsync();

                foreach (var term in terms)
                {
                    context.Terms.Remove(term);
                }
                await context.SaveChangesAsync();

                // Insert new term
                await context.Terms.AddRangeAsync(createdTerms);

                // Update set
                set.Id = id;
                context.Set<Set>().Update(set);
                await context.SaveChangesAsync();

                return set;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (ApplicationDbContext context = _quizletCloneDbContextFactory.CreateDbContext())
            {
                // Remove all terms belong to this set
                var terms = await context.Terms
                        .Where(s => s.SetId == id)
                        .ToListAsync();

                List<int> termIds = new List<int>();

                foreach (var term in terms)
                {
                    termIds.Add(term.Id);
                }

                var userLearningTerms = await context.LearningTerms
                        .Where(lt => termIds.Contains(lt.TermId))
                        .ToListAsync();

                foreach (var userLearningTerm in userLearningTerms)
                {
                    context.LearningTerms.Remove(userLearningTerm);
                }
                await context.SaveChangesAsync();

                foreach (var term in terms)
                {
                    context.Terms.Remove(term);
                }
                await context.SaveChangesAsync();

                // Remove set
                Set? set = await context.Set<Set>()
                    .Where(s => s.Id == id)
                    .FirstOrDefaultAsync();

                if (set != null)
                {
                    context.Set<Set>().Remove(set);
                }

                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<Term?> GetRandomLearningTerm(int setId, string userId)
        {
            using (ApplicationDbContext context = _quizletCloneDbContextFactory.CreateDbContext())
            {
                Term? term = await context.Terms
                    .FromSql($"select top(1) Terms.* from Terms left join LearningTerms on Terms.Id = LearningTerms.TermId where Terms.SetId = {setId} and (LearningTerms.UserId = {userId} or LearningTerms.UserId is null) and (LearningTerms.Learned != 1 or LearningTerms.Learned is null) order by newID()")
                    .FirstOrDefaultAsync();

                if (term == null)
                {
                    return null;
                }

                List<string> randomAnswers = await context.Terms
                    .Where(t => t.SetId == setId && t.Id != term.Id && !t.Answer.Equals(term.Answer))
                    .OrderBy(s => Guid.NewGuid())
                    .Take(3)
                    .Select(t => t.Answer)
                    .Distinct()
                    .ToListAsync();

                term.Choices = new List<string>();
                foreach (var randomAnswer in randomAnswers) 
                {
                    term.Choices.Add(randomAnswer);
                }

                return term;
            }
        }

        public async Task ReportLearningProgress(int termId, string userId, bool correct)
        {
            using (ApplicationDbContext context = _quizletCloneDbContextFactory.CreateDbContext())
            {
                UserLearningTerm? userLearningTerm = await context.LearningTerms
                    .Where(lt => lt.UserId.Equals(userId) && lt.TermId == termId)
                    .FirstOrDefaultAsync();

                // Create if not exists
                if (userLearningTerm == null)
                {
                    UserLearningTerm newUserLearningTerm = new UserLearningTerm()
                    {
                        TermId = termId,
                        UserId = userId,
                        Remained = correct ? 3 : 5, // Answer correct 3 times in a row if you're right, 5 times if you wrong
                        Learned = false
                    };

                    context.Add(newUserLearningTerm);
                } 
                else // Else we'll update
                {
                    if (correct)
                    {
                        userLearningTerm.Remained--; // Remove 1 if you're true

                        if (userLearningTerm.Remained == 0)
                        {
                            userLearningTerm.Learned = true; // You have 'mastered' this term
                        }
                    }
                    else
                    {
                        userLearningTerm.Remained = 5; // Set it back to 5
                    }

                    context.Update(userLearningTerm);
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task<int> CountLearningTermProgress(int setId, string userId)
        {
            using (ApplicationDbContext context = _quizletCloneDbContextFactory.CreateDbContext())
            {
                int count = await context.Database
                    .SqlQuery<int>($"select count(distinct LearningTerms.Id) as Value from LearningTerms join Terms on LearningTerms.TermId = Terms.Id where LearningTerms.UserId = {userId} and Terms.SetId = {setId} and LearningTerms.Learned = 1")
                    .FirstOrDefaultAsync();

                return count;
            }
        }
    }
}

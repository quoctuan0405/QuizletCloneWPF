using Server.Model;
using Server.Data;
using Server.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace Server.Services
{
    public class TermService : GenericDataService<Term>
    {
        public TermService(IDbContextFactory<ApplicationDbContext> applicationDbContext) : base(applicationDbContext)
        {
        }

        public async Task<IEnumerable<Term>> GetTermsBySetId(int setId)
        {
            using (ApplicationDbContext context = _quizletCloneDbContextFactory.CreateDbContext()) {
                IEnumerable<Term> entities = context.Set<Term>()
                    .Where(t => t.SetId == setId)
                    .ToList();

                return entities;
            }
        }
    }
}

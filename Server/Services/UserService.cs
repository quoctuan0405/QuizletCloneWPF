
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Model;
using Server.Services.Base;

namespace Server.Services
{
    public class UserService
    {
        protected readonly IDbContextFactory<ApplicationDbContext> _quizletCloneDbContextFactory;

        public UserService(IDbContextFactory<ApplicationDbContext> quizletCloneDbContextFactory) 
        {
            _quizletCloneDbContextFactory = quizletCloneDbContextFactory;
        }

        public async Task<ApplicationUser> Get(string id)
        {
            using (ApplicationDbContext context = _quizletCloneDbContextFactory.CreateDbContext())
            {
                ApplicationUser entity = await context.Set<ApplicationUser>().FirstOrDefaultAsync((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<ApplicationUser> GetByUsername(string username)
        {
            using (ApplicationDbContext context = _quizletCloneDbContextFactory.CreateDbContext())
            {
                ApplicationUser entity = await context.Set<ApplicationUser>().FirstOrDefaultAsync((e) => e.UserName.Equals(username));
                return entity;
            }
        }
    }
}

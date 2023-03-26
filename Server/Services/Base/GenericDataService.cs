using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QuizletClone.Domain.Services;
using Server.Data;
using Server.Model;

namespace Server.Services.Base
{
    public class GenericDataService<T> : IDataService<T> where T : DomainObject
    {
        protected readonly IDbContextFactory<ApplicationDbContext> _quizletCloneDbContextFactory;

        public GenericDataService(IDbContextFactory<ApplicationDbContext> quizletCloneDbContextFactory)
        {
            _quizletCloneDbContextFactory = quizletCloneDbContextFactory;
        }

        public async Task<T> Create(T entity)
        {
            using (ApplicationDbContext context = _quizletCloneDbContextFactory.CreateDbContext())
            {
                EntityEntry<T> result = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

                return result.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (ApplicationDbContext context = _quizletCloneDbContextFactory.CreateDbContext())
            {
                T entity = context.Set<T>().FirstOrDefault((e) => e.Id == id);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<T> Get(int id)
        {
            using (ApplicationDbContext context = _quizletCloneDbContextFactory.CreateDbContext())
            {
                T entity = context.Set<T>().FirstOrDefault((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (ApplicationDbContext context = _quizletCloneDbContextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = context.Set<T>().ToList();
                return entities;
            }
        }

        public async Task<T> Update(int id, T entity)
        {
            using (ApplicationDbContext context = _quizletCloneDbContextFactory.CreateDbContext())
            {
                entity.Id = id;
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }
    }
}

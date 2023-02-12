using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QuizletClone.Domain.Model;
using QuizletClone.Domain.Services;

namespace QuizletClone.EntityFramework.Services
{
    public class GenericDataService<T> : IDataService<T> where T : DomainObject
    {
        private readonly QuizletCloneDbContextFactory _quizletCloneDbContextFactory;

        public GenericDataService(QuizletCloneDbContextFactory quizletCloneDbContextFactory)
        {
            _quizletCloneDbContextFactory = quizletCloneDbContextFactory;
        }

        public async Task<T> Create(T entity)
        {
            using (QuizletCloneDbContext context = _quizletCloneDbContextFactory.CreateDbContext())
            {
                EntityEntry<T> result = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

                return result.Entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (QuizletCloneDbContext context = _quizletCloneDbContextFactory.CreateDbContext())
            {
                T entity = context.Set<T>().FirstOrDefault((e) => e.Id == id);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }

        public async Task<T> Get(int id)
        {
            using (QuizletCloneDbContext context = _quizletCloneDbContextFactory.CreateDbContext())
            {
                T entity = context.Set<T>().FirstOrDefault((e) => e.Id == id);
                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (QuizletCloneDbContext context = _quizletCloneDbContextFactory.CreateDbContext())
            {
                IEnumerable<T> entities = context.Set<T>().ToList();
                return entities;
            }
        }

        public async Task<T> Update(int id, T entity)
        {
            using (QuizletCloneDbContext context = _quizletCloneDbContextFactory.CreateDbContext())
            {
                entity.Id= id;
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }
    }
}

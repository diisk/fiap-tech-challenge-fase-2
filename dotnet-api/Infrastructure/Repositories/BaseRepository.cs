using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : EntityBase
    {
        protected readonly OnlyWriteDbContext onlyWriteDbContext;
        protected readonly OnlyReadDbContext onlyReadDbContext;
        protected readonly DbSet<T> onlyReadDbSet;
        protected readonly DbSet<T> onlyWriteDbSet;

        protected BaseRepository(OnlyWriteDbContext onlyWriteDbContext, OnlyReadDbContext onlyReadDbContext)
        {
            this.onlyWriteDbContext = onlyWriteDbContext;
            this.onlyReadDbContext = onlyReadDbContext;
            this.onlyReadDbSet = onlyReadDbContext.Set<T>();
            this.onlyWriteDbSet = onlyWriteDbContext.Set<T>();
        }

        public ICollection<T> AddRange(ICollection<T> entities)
        {
            onlyWriteDbContext.BulkInsert(entities);
            return entities;
        }

        public void Delete(T entity)
        {
            onlyWriteDbSet.Remove(entity);
            onlyWriteDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var foundEntity = FindById(id);
            if (foundEntity != null)
                Delete(foundEntity);
        }

        public ICollection<T> FindAll()
            => onlyReadDbSet.Where(entity=>!entity.Removed).ToList();


        public T? FindById(int id)
            => onlyReadDbSet.FirstOrDefault(e => e.ID == id);


        public T Save(T entity)
        {
            return GenericSave(entity,true);
        }

        public T GenericSave(T entity, bool saveChanges)
        {
            if (entity.ID > 0)
            {
                onlyWriteDbSet.Update(entity);
            }
            else
            {
                onlyWriteDbSet.Add(entity);
            }

            if (saveChanges)
            {
                onlyWriteDbContext.SaveChanges();
            }
            
            return entity;
        }

        public List<T> SaveAll(List<T> entities)
        {
            foreach (var entity in entities)
            {
                GenericSave(entity,false);
            }
            onlyWriteDbContext.SaveChanges();
            return entities;
        }

        public ICollection<T> UpdateRange(ICollection<T> entities)
        {
            onlyWriteDbContext.BulkUpdate(entities);
            return entities;
        }
    }
}

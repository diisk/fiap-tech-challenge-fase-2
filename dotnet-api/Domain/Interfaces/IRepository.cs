using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : EntityBase
    {
        ICollection<T> FindAll();

        T? FindById(int id);

        T Save(T entity);
        List<T> SaveAll(List<T> entities);

        void Delete(T entity);

        void Delete(int id);

        ICollection<T> AddRange(ICollection<T> entities);
        ICollection<T> UpdateRange(ICollection<T> entities);
    }
}

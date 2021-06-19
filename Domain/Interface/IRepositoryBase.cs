using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface
{
    public interface IRepositoryBase<T> where T : class
    {
        IQueryable<T> GetAll(bool trackChanges);
        IQueryable<T> FindBy(Expression<Func<T, bool>> expression, bool trackChanges);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> SaveAsync(string username, string ip);
    }
}

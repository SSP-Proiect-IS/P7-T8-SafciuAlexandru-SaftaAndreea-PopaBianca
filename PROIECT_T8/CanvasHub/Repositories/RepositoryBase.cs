using CanvasHub.Models;
using CanvasHub.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CanvasHub.Repositories
{
   
        public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
        {
            protected CanvasHubContext _applicationDbContext { get; set; }

            public RepositoryBase(CanvasHubContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            public IQueryable<T> FindAll()
            {
                return _applicationDbContext.Set<T>().AsNoTracking();
            }

            public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
            {
                return _applicationDbContext.Set<T>().Where(expression).AsNoTracking();
            }

            public void Create(T entity)
            {
                _applicationDbContext.Set<T>().Add(entity);
            }

            public void Update(T entity)
            {
                _applicationDbContext.Set<T>().Update(entity);
            }

            public void Delete(T entity)
            {
                _applicationDbContext.Set<T>().Remove(entity);
            }
        }
    }


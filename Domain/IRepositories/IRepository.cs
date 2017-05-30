using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Domain.IRepositories
{
   public interface IRepository
    {
    }

    public interface IRepository<TEntity,TPrimaryKey>:IRepository where TEntity : Entity<TPrimaryKey>
    {
        List<TEntity> GetAllList();
        List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(TPrimaryKey id);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entity);
        TEntity InsertOrUpdate(TEntity entity);
        void Delete(TEntity entity);
        void Delete(TPrimaryKey id);
        void Delete(Expression<Func<TEntity, bool>> where, bool autoSave = true);
        IQueryable<TEntity> LoadPageList(int startPage, int pageSize, out int rowCount, Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> order);
    }

    public interface IRepository<TEntity>:IRepository<TEntity,Guid> where TEntity : Entity
    {

    }
}

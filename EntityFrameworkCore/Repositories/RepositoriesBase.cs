using Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.Repositories
{
    public abstract class RepositoriesBase<TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {
        protected readonly WeaverDbContext _dbContext;

        public RepositoriesBase(WeaverDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// 要删除的实体
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// 删除实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public void Delete(TPrimaryKey id)
        {
            _dbContext.Set<TEntity>().Remove(Get(id));
        }

        /// <summary>
        /// 根据lambda表达式条件获取单个实体
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Get(TPrimaryKey id)
        {
            return _dbContext.Set<TEntity>().FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        /// <summary>
        /// 获取实体集合
        /// </summary>
        /// <returns></returns>
        public List<TEntity> GetAllList()
        {
            return _dbContext.Set<TEntity>().ToList();
        }

        /// <summary>
        /// 根据lambda表达式获取实体集合
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbContext.Set<TEntity>().Where(predicate).ToList();
        }

        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Insert(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
            return entity;
        }

        /// <summary>
        /// 新增或更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity InsertOrUpdate(TEntity entity)
        {
            if (Get(entity.Id) != null)
                return Update(entity);

            return Insert(entity);
        }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public TEntity Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        /// <summary>
        ///事务性保存 
        /// </summary>
        public void Save()
        {
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// 根据主键构建判断表达式
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        protected static Expression<Func<TEntity,bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));
            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam,"Id"),
                Expression.Constant(id,typeof(TPrimaryKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }

        /// <summary>
        /// 根据条件删除实体
        /// </summary>
        /// <param name="where">lambda表达式</param>
        /// <param name="autoSave">是否自动保存</param>
        public void Delete(Expression<Func<TEntity,bool>>where,bool autoSave = true)
        {
            _dbContext.Set<TEntity>().Where(where).ToList().ForEach(i => _dbContext.Set<TEntity>().Remove(i));
        }

        public IQueryable<TEntity> LoadPageList(int startPage,int pageSize,out int rowCount,Expression<Func<TEntity,bool>>where,Expression<Func<TEntity,object>>order = null)
        {
            var result = from p in _dbContext.Set<TEntity>()
                                 select p;
            if (where != null)
                result = result.Where(where);
            if (order != null)
                result = result.OrderBy(order);
            else
                result = result.OrderBy(m => m.Id);

            rowCount = result.Count();
            return result.Skip((startPage - 1) * pageSize).Take(pageSize);
        }
    }

    public abstract class RepositoriesBase<TEntity>: RepositoriesBase<TEntity,Guid> where TEntity : Entity
    {
        public RepositoriesBase(WeaverDbContext dbContext) : base(dbContext) { }
    }
}
